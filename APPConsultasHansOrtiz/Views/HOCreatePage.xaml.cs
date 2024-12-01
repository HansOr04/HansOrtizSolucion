using System.Text;
using System.Text.Json;
using APPConsultasHansOrtiz.Models;

namespace APPConsultasHansOrtiz.Views;

public partial class HOCreatePage : ContentPage
{
    private readonly HttpClient _httpClient;
    private const string API_URL = "https://localhost:7019"; // Reemplaza con tu URL de API

    public HOCreatePage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(API_URL);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (ValidateInputs() == false)
            return;

        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;
        BtnSave.IsEnabled = false;

        try
        {
            var newContact = new HO_Contacto
            {
                FirstName = EntryFirstName.Text,
                LastName = EntryLastName.Text,
                PhoneNumber = EntryPhone.Text,
                Email = EntryEmail.Text
            };

            var json = JsonSerializer.Serialize(newContact);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/HO_Contacto", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Contacto creado correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo crear el contacto", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al crear el contacto: {ex.Message}", "OK");
        }
        finally
        {
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
            BtnSave.IsEnabled = true;
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