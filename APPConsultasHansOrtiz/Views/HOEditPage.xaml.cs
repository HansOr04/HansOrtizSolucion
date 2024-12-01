using System.Text;
using System.Text.Json;
using APPConsultasHansOrtiz.Models;

namespace APPConsultasHansOrtiz.Views;

public partial class HOEditPage : ContentPage
{
    private readonly HttpClient _httpClient;
    private const string API_URL = "https://localhost:7019"; // Reemplaza con tu URL de API
    private int _contactId;

    public HOEditPage(HO_Contacto contacto)
    {
        InitializeComponent();
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(API_URL);
        
        // Cargar los datos del contacto en los campos
        LoadContactData(contacto);
    }

    private void LoadContactData(HO_Contacto contacto)
    {
        _contactId = contacto.IdHO_Contactos;
        EntryId.Text = contacto.IdHO_Contactos.ToString();
        EntryFirstName.Text = contacto.FirstName;
        EntryLastName.Text = contacto.LastName;
        EntryPhone.Text = contacto.PhoneNumber;
        EntryEmail.Text = contacto.Email;
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        if (ValidateInputs() == false)
            return;

        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;
        BtnUpdate.IsEnabled = false;

        try
        {
            var updatedContact = new HO_Contacto
            {
                IdHO_Contactos = _contactId,
                FirstName = EntryFirstName.Text,
                LastName = EntryLastName.Text,
                PhoneNumber = EntryPhone.Text,
                Email = EntryEmail.Text
            };

            var json = JsonSerializer.Serialize(updatedContact);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/HO_Contacto/{_contactId}", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Contacto actualizado correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar el contacto", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al actualizar el contacto: {ex.Message}", "OK");
        }
        finally
        {
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
            BtnUpdate.IsEnabled = true;
        }
    }

    private bool ValidateInputs()
    {
        if (string.IsNullOrWhiteSpace(EntryFirstName.Text))
        {
            DisplayAlert("Error", "El nombre es requerido", "OK");
            return false;
        }

        if (string.IsNullOrWhiteSpace(EntryLastName.Text))
        {
            DisplayAlert("Error", "El apellido es requerido", "OK");
            return false;
        }

        if (string.IsNullOrWhiteSpace(EntryPhone.Text))
        {
            DisplayAlert("Error", "El teléfono es requerido", "OK");
            return false;
        }

        if (string.IsNullOrWhiteSpace(EntryEmail.Text))
        {
            DisplayAlert("Error", "El email es requerido", "OK");
            return false;
        }

        return true;
    }
}