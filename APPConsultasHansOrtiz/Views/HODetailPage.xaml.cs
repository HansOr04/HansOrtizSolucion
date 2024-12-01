using APPConsultasHansOrtiz.Models;

namespace APPConsultasHansOrtiz.Views;

public partial class HODetailPage : ContentPage
{
    private readonly HttpClient _httpClient;
    private HO_Contacto _currentContact;

    public HODetailPage(HO_Contacto contacto)
    {
        InitializeComponent();
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7019");
        _currentContact = contacto;
        LoadContactDetails();
    }

    private void LoadContactDetails()
    {
        LabelId.Text = _currentContact.IdHO_Contactos.ToString();
        LabelFirstName.Text = _currentContact.FirstName;
        LabelLastName.Text = _currentContact.LastName;
        LabelPhone.Text = _currentContact.PhoneNumber;
        LabelEmail.Text = _currentContact.Email;
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HOEditPage(_currentContact));
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmar",
            "¿Está seguro que desea eliminar este contacto?",
            "Sí", "No");

        if (!answer)
            return;

        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;
        BtnDelete.IsEnabled = false;
        BtnEdit.IsEnabled = false;

        try
        {
            var response = await _httpClient.DeleteAsync($"/api/HO_Contacto/{_currentContact.IdHO_Contactos}");

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Contacto eliminado correctamente", "OK");
                // Regresamos a la página anterior (lista de contactos)
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo eliminar el contacto", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al eliminar el contacto: {ex.Message}", "OK");
        }
        finally
        {
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
            BtnDelete.IsEnabled = true;
            BtnEdit.IsEnabled = true;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Recargar los detalles del contacto cuando volvemos de la página de edición
        LoadContactDetails();
    }
}