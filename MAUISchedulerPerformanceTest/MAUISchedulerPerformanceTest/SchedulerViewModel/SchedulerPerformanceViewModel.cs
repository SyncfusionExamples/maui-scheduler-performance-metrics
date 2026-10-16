using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MAUISchedulerPerformanceTest
{
    public class SchedulerPerformanceViewModel :INotifyPropertyChanged
    {
        private readonly Random random = new();

        private ObservableCollection<Meeting> appointments = new ObservableCollection<Meeting>();

        public ObservableCollection<Meeting> Appointments
        {
            get => appointments;
            set
            {
                appointments = value;
                OnPropertyChanged(nameof(Appointments));
            }
        }

        private ObservableCollection<Employee> resources = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> Resources
        {
            get => resources;
            set
            {
                resources = value;
                OnPropertyChanged(nameof(Resources));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SchedulerPerformanceViewModel(int appointmentCount)
        {
            GenerateAppointments(appointmentCount);
        }

        public SchedulerPerformanceViewModel()
        {
            GenerateResources();
        }

        private void GenerateResources()
        {
            Resources = new ObservableCollection<Employee>();

            int resourceId = 1;

            // Level 1 - 10 Departments
            for (int department = 1; department <= 10; department++)
            {
                int departmentId = resourceId++;

                Resources.Add(new Employee
                {
                    Id = departmentId,
                    Name = $"Department {department}",
                    BackgroundColor = new SolidColorBrush(Colors.DarkBlue),
                    ForegroundColor = new SolidColorBrush(Colors.White)
                });

                // Level 2 - 10 Teams per Department
                for (int team = 1; team <= 10; team++)
                {
                    int teamId = resourceId++;

                    Resources.Add(new Employee
                    {
                        Id = teamId,
                        GroupId = departmentId,
                        Name = $"Team {department}-{team}",
                        BackgroundColor = new SolidColorBrush(Colors.DarkGreen),
                        ForegroundColor = new SolidColorBrush(Colors.White)
                    });

                    // Level 3
                    // total members = 10 * 10 * 9 = 900
                    for (int member = 1; member <= 9; member++)
                    {
                        Resources.Add(new Employee
                        {
                            Id = resourceId,
                            GroupId = teamId,
                            Name = $"Emp {department}-{team}-{member}",
                            BackgroundColor = new SolidColorBrush(GetRandomColor()),
                            ForegroundColor = new SolidColorBrush(Colors.White)
                        });

                        resourceId++;
                    }
                }
            }

            // Total Resources = 10 + 100 + 900 = 1010
            // Trim to 1000
            while (Resources.Count > 1000)
            {
                Resources.RemoveAt(Resources.Count - 1);
            }
        }

        private void GenerateAppointments(int count)
        {
            Appointments = new ObservableCollection<Meeting>();

            DateTime startDate = new DateTime(2023, 1, 1);

            if (count >= 100000)
            {
                int totalDays = (startDate.AddYears(30) - startDate).Days;

                for (int i = 0; i < count; i++)
                {
                    // Move to next day after every 10 appointments

                    int dayOffset = i / 10;

                    if (dayOffset >= totalDays)
                        throw new InvalidOperationException($"Cannot fit {count} appointments into 30 years with a maximum of 10 appointments per day.");

                    DateTime day = startDate.AddDays(dayOffset);
                    DateTime startTime = day.AddHours(random.Next(8, 20)).AddMinutes(random.Next(60));
                    DateTime endTime = startTime.AddHours(random.Next(1, 4));
                    Appointments.Add(new Meeting
                    {
                        Id = i,
                        Subject = $"Meeting {i + 1}",
                        StartTime = startTime,
                        EndTime = startTime.AddHours(random.Next(1, 4)),
                        Notes = $"Performance appointment {i + 1}",
                        Location = $"Room {random.Next(1, 100)}",
                        IsAllDay = false,
                        BackgroundColor = new SolidColorBrush(GetRandomColor()),
                    });
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    int monthOffset = i % 48; // 4 years × 12 months

                    DateTime monthStart = startDate.AddMonths(monthOffset);

                    DateTime startTime = monthStart
                        .AddDays(random.Next(DateTime.DaysInMonth(monthStart.Year, monthStart.Month)))
                        .AddHours(random.Next(24))
                        .AddMinutes(random.Next(60));

                    Appointments.Add(new Meeting
                    {
                        Id = i,
                        Subject = $"Meeting {i + 1}",
                        StartTime = startTime,
                        EndTime = startTime.AddHours(random.Next(1, 4)),
                        Notes = $"Performance appointment {i + 1}",
                        Location = $"Room {random.Next(1, 100)}",
                        IsAllDay = false,
                        BackgroundColor = new SolidColorBrush(GetRandomColor()),
                    });
                }
            }
        }

        private Color GetRandomColor()
        {
            return Color.FromRgb(random.Next(40, 255),
                                 random.Next(40, 255),
                                 random.Next(40, 255));
        }
    }    
}
