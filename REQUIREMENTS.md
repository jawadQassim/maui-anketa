# Соответствие требованиям

| Требование | Реализация в MainPage.xaml |
|---|---|
| ≥2 Label; Text, FontSize, FontAttributes | Заголовок, описание и заголовки разделов |
| 2 Entry; Placeholder, Keyboard, MaxLength | NameEntry (Text, 60), EmailEntry (Email, 120) |
| Editor; Placeholder, AutoSize, MaxLength | GoalEditor, TextChanges, 500 |
| Picker; Title, ≥3 пункта | CoursePicker, три строки Picker.Items |
| 3 RadioButton; Content, GroupName, Value | BeginnerRadio, IntermediateRadio, AdvancedRadio; Experience |
| 2 CheckBox; IsChecked, Color | WebCheckBox, MobileCheckBox |
| Switch; IsToggled, OnColor | ReminderSwitch |
| DatePicker; Format, Date, MinimumDate, MaximumDate | StartDatePicker, 01.11.2026 в диапазоне 2026–2030 |
| Stepper; Minimum, Maximum, Value, Increment | HoursStepper: 1, 40, 5, 1 |
| Image; Source, Aspect, HeightRequest | course.png (генерируется из course.svg), AspectFit, 120 |
| 2 Button; Text, Clicked | OnShowResultClicked, OnClearClicked |
| Grid с RowDefinitions и ColumnDefinitions | Три Grid; у каждого непосредственного ребёнка заданы Grid.Row и Grid.Column |
| StackLayout со Spacing | VerticalStackLayout, включая основной контейнер |
| Margin и Padding | Заголовки/поля и контейнеры Border/VerticalStackLayout |
| ScrollView | FormScroll охватывает всю анкету и результат |
| x:Name | Все элементы, используемые C# или x:Reference |
| x:Reference, Path, Mode | HoursStepper.Value → Label.Text, OneWay |
| Изменение IsVisible/IsEnabled | Подсказка зависит от ReminderSwitch; результат появляется после проверки |
| Проверка ≥2 полей | Имя, почта, выбор курса, наличие даты |
| Сводка всех ответов | 9 строк ResultLabel |
| Полный сброс | OnClearClicked, включая ошибки, сводку, дату, часы и переключатели |
| Code-behind | Вся логика в MainPage.xaml.cs |

Дополнительные события TextChanged, SelectedIndexChanged, CheckedChanged,
DateSelected, ValueChanged, Toggled подключены к OnAnswersChanged.
