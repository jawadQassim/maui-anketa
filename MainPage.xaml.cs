using System.Net.Mail;

namespace AnketaMaui;

public partial class MainPage : ContentPage
{
    private bool _isReady;
    private readonly DateTime _initialDate;
    private readonly double _initialHours;

    public MainPage()
    {
        InitializeComponent();
        // Defaults are declared in XAML. Preserve them for an exact reset.
        _initialDate = StartDatePicker.Date ?? new DateTime(2026, 11, 1);
        _initialHours = HoursStepper.Value;
        _isReady = true;
    }

    // Several event argument types inherit EventArgs, so one handler can
    // invalidate the previous result whenever an answer changes.
    private void OnAnswersChanged(object? sender, EventArgs e)
    {
        if (!_isReady) return; // XAML can raise events during construction.
        ClearOutput();
    }

    private void ClearOutput()
    {
        ValidationLabel.Text = string.Empty;
        ValidationLabel.IsVisible = false;
        ResultLabel.Text = string.Empty;
        ResultBorder.IsVisible = false;
    }

    private async void OnShowResultClicked(object sender, EventArgs e)
    {
        ClearOutput();
        var name = (NameEntry.Text ?? string.Empty).Trim();
        var email = (EmailEntry.Text ?? string.Empty).Trim();
        var errors = new List<string>();
        VisualElement? firstInvalid = null;

        if (name.Length < 2)
        {
            errors.Add("Введите имя: минимум 2 символа.");
            firstInvalid = NameEntry;
        }
        if (!MailAddress.TryCreate(email, out var address) || address.Address != email)
        {
            errors.Add("Введите корректный адрес электронной почты.");
            firstInvalid ??= EmailEntry;
        }
        if (CoursePicker.SelectedIndex < 0)
        {
            errors.Add("Выберите курс.");
            firstInvalid ??= CoursePicker;
        }
        if (StartDatePicker.Date is null)
        {
            errors.Add("Выберите дату начала.");
            firstInvalid ??= StartDatePicker;
        }
        if (errors.Count > 0)
        {
            ValidationLabel.Text = string.Join(Environment.NewLine, errors);
            ValidationLabel.IsVisible = true;
            if (firstInvalid is not null)
            {
                await FormScroll.ScrollToAsync(firstInvalid, ScrollToPosition.Center, true);
                firstInvalid.Focus();
            }
            await DisplayAlertAsync("Проверьте анкету", ValidationLabel.Text, "Понятно");
            return;
        }

        var interests = new List<string>();
        if (WebCheckBox.IsChecked) interests.Add("Веб-разработка");
        if (MobileCheckBox.IsChecked) interests.Add("Мобильные приложения");
        var level = new[] { BeginnerRadio, IntermediateRadio, AdvancedRadio }
            .FirstOrDefault(radio => radio.IsChecked)?.Value?.ToString() ?? "Не указан";
        var goal = (GoalEditor.Text ?? string.Empty).Trim();
        ResultLabel.Text = string.Join(Environment.NewLine, new[]
        {
            $"Имя: {name}",
            $"Электронная почта: {email}",
            $"Курс: {CoursePicker.SelectedItem}",
            $"Уровень: {level}",
            $"Интересы: {(interests.Count == 0 ? "Не выбраны" : string.Join(", ", interests))}",
            $"Дата начала: {StartDatePicker.Date:dd.MM.yyyy}",
            $"Часов в неделю: {HoursStepper.Value:F0}",
            $"Напоминания: {(ReminderSwitch.IsToggled ? "Да" : "Нет")}",
            $"Цель: {(goal.Length == 0 ? "Не указана" : goal)}"
        });
        ResultBorder.IsVisible = true;
        // Wait for the newly visible summary to enter the layout.
        Dispatcher.Dispatch(async () =>
            await FormScroll.ScrollToAsync(ResultBorder, ScrollToPosition.End, true));
    }

    private async void OnClearClicked(object sender, EventArgs e)
    {
        _isReady = false;
        NameEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
        GoalEditor.Text = string.Empty;
        CoursePicker.SelectedIndex = -1;
        IntermediateRadio.IsChecked = false;
        AdvancedRadio.IsChecked = false;
        BeginnerRadio.IsChecked = true;
        WebCheckBox.IsChecked = false;
        MobileCheckBox.IsChecked = false;
        ReminderSwitch.IsToggled = false;
        StartDatePicker.Date = _initialDate;
        HoursStepper.Value = _initialHours;
        ClearOutput();
        _isReady = true;
        await FormScroll.ScrollToAsync(0, 0, true);
    }
}
