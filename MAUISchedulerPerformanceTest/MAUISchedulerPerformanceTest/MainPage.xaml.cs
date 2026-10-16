namespace MAUISchedulerPerformanceTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnButtonClicked(object? sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                switch (button.Text)
                {
                    case "Without Appointment":
                        await Navigation.PushAsync(new WithoutAppointment());
                        break;
                    case "1K Appointments":
                        await Navigation.PushAsync(new Scheduler1KAppointmentsPage());
                        break;
                    case "5K Appointments":
                        await Navigation.PushAsync(new Scheduler5KAppointmentsPage());
                        break;
                    case "10K Appointments":
                        await Navigation.PushAsync(new Scheduler10KAppointmentsPage());
                        break;
                    case "50K Appointments":
                        await Navigation.PushAsync(new Scheduler50KAppointmentsPage());
                        break;
                    case "100K Appointments":
                        await Navigation.PushAsync(new Scheduler100KAppointmentsPage());
                        break;
                    case "Hierarchical Resources":
                        await Navigation.PushAsync(new HierarchicalResources());
                        break;
                }
            }
        }
    }
}
