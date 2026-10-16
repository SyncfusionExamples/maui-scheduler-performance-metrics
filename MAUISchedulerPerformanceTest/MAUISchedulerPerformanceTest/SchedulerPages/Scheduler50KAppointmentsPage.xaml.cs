using Syncfusion.Maui.Scheduler;
using System.Diagnostics;

namespace MAUISchedulerPerformanceTest;

public partial class Scheduler50KAppointmentsPage : ContentPage
{
    Stopwatch stopwatch;
    string sizeText = string.Empty;

    public Scheduler50KAppointmentsPage()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        InitializeComponent();
        BindingContext = new SchedulerPerformanceAgendaViewModel();
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