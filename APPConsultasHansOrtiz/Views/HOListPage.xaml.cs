using APPConsultasHansOrtiz.Models;
using APPConsultasHansOrtiz.Services;

namespace APPConsultasHansOrtiz.Views;

public partial class HOListPage : ContentPage
{
    private readonly ApiService _apiService;

    public HOListPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadContacts();
    }

    private async Task LoadContacts()
    {
        if (LoadingIndicator.IsVisible)
            return;

        try
        {
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;

            var contacts = await _apiService.GetAllContactos();

            // Debug: Imprimir los datos recibidos
            foreach (var contact in contacts)
            {
                System.Diagnostics.Debug.WriteLine($"====== Contact Data ======");
                System.Diagnostics.Debug.WriteLine($"ID: {contact.IdHO_Contactos}");
                System.Diagnostics.Debug.WriteLine($"FirstName: '{contact.FirstName}'");
                System.Diagnostics.Debug.WriteLine($"LastName: '{contact.LastName}'");
                System.Diagnostics.Debug.WriteLine($"Phone: {contact.PhoneNumber}");
                System.Diagnostics.Debug.WriteLine($"Email: {contact.Email}");
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                ContactsCollection.ItemsSource = null;  // Limpiar primero
                ContactsCollection.ItemsSource = contacts;
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al cargar los contactos: {ex.Message}", "OK");
            System.Diagnostics.Debug.WriteLine($"Error loading contacts: {ex}");
        }
        finally
        {
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
        }
    }

    private async void OnContactTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is HO_Contacto contact)
        {
            await Navigation.PushAsync(new HODetailPage(contact));
        }
    }

    private async void OnAddContactClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HOCreatePage());
    }
}