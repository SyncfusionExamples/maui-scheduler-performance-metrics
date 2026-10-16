using System.Diagnostics;

namespace MAUISchedulerPerformanceTest;

public partial class Scheduler1KAppointmentsPage : ContentPage
{
    Stopwatch stopwatch;
    string sizeText = string.Empty;

    public Scheduler1KAppointmentsPage()
	{
        stopwatch = new Stopwatch();
        stopwatch.Start();
        InitializeComponent();
        BindingContext = new SchedulerPerformanceViewModel(1000);
    }

    protected override void OnAppearing()
    {
        GC.Collect();
        base.OnAppearing();
        RefreshStats();
    }

    private void RefreshStats()
    {
        double heapSize = (GC.GetTotalMemory(false)) / (1024.0 * 1024.0);
        sizeText = heapSize.ToString();
    }

    private void scheduler_Loaded(object? sender, EventArgs e)
    {
        stopwatch.Stop();
        DisplayAlertAsync("Time taken to load the Scheduler", stopwatch.Elapsed.TotalSeconds.ToString() + "\nMemory: " + sizeText, "Cancel");
    }
}