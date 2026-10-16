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
                }
            }
        }
    }
}
