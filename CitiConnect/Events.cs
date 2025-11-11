using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CitiConnect
{
    public partial class Events : Form
    {
        
        private Queue<Event> eventQueue = new Queue<Event>(); // A queue to store events in the order they are added
        private SortedSet<Event> priorityEvents = new SortedSet<Event>(new EventComparer()); // // SortedSet to manage priority events based on a custom comparison
        private SortedDictionary<DateTime, List<Event>> eventsByDate = new SortedDictionary<DateTime, List<Event>>(); // To organize events by their date
        private HashSet<string> categories = new HashSet<string>(); // HashSet to store the categories associated with the events

        // This code was adapted from GeeksforGeeks
        // Link: https://www.geeksforgeeks.org/hashset-in-c-sharp-with-examples/
        // Date: 8 March 2024
        // Accessed: 14 October 2024
        private HashSet<string> uniqueCategories = new HashSet<string> // HashSet to store predefined unique categories
        {
        "All Categories",
        "Crime and Safety",
        "Environmental Concerns",
        "Roads",
        "Sanitation",
        "Utilities",
        "Other"
        };

        private Dictionary<Event, bool> eventDetailsVisible = new Dictionary<Event, bool>(); // // Dictionary to track whether event details are visible or not
        private Dictionary<string, int> searchPatterns = new Dictionary<string, int>(); // To track category searches

        // Declaring UI elements
        private ComboBox cmbCategory;
        private DateTimePicker dtpDate;
        private Button btnSearch;
        private Panel searchPanel;
        private Panel recommendationPanel;

        public Events()
        {
            InitializeComponent();

            // Adding the UI elements used to search for a category and date
            AddSearchUI();

            // Adding the recommendations panel to dipslay recommendations for events
            AddRecommendationPanel();

            // Adding events to be searched for and viewed
            AddEvents();

            // Display all events by default
            DisplayUpcomingEvents();
        }


        // This code was adapted from Go4Expert
        // Link: https://www.go4expert.com/articles/lists-queues-stacks-sets-c-sharp-t30028/
        // Author: shabbir
        // Date: 21 March 2024
        // Adding sample events with their relevant data
        private void AddEvents()
        {
            var events = new List<Event>  // List to store events
    {
    new Event("International Fire Safety Week",
        "Join us for a week-long series of activities aimed at raising fire prevention awareness in our community.\nParticipate in workshops and demonstrations.",
        "Midrand",
        new DateTime(2024, 10, 6),
        new DateTime(2024, 10, 4, 12, 0, 0),
        "Crime and Safety"),

    new Event("Power Outage in Midrand",
        "Notice of a power outage scheduled from 6 PM to 8 PM due to essential maintenance work on the electricity grid.",
        "Midrand",
        new DateTime(2024, 10, 12, 18, 0, 0),
        new DateTime(2024, 10, 10, 9, 0, 0),
        "Utilities"),

    new Event("Water Saving Program",
        "Introducing new measures aimed at conserving water in our community. Join us to learn more about\neffective water usage and the importance of sustainability.",
        "Pretoria",
        new DateTime(2024, 10, 15),
        new DateTime(2024, 10, 8, 14, 0, 0),
        "Sanitation"),

    new Event("Crime Prevention Workshop",
        "Attend our interactive workshop designed to equip residents with safety tips and techniques\nto prevent crime in their neighborhoods.",
        "Soweto",
        new DateTime(2024, 10, 20),
        new DateTime(2024, 10, 12, 9, 0, 0),
        "Crime and Safety"),

    new Event("Community Work Programme Launch",
        "Join us for the launch of the Reimagined Community Work Programme. Discover how this initiative\nwill create job opportunities and promote community engagement.",
        "Johannesburg",
        new DateTime(2024, 11, 1),
        new DateTime(2024, 10, 20, 10, 0, 0),
        "Other"),

    new Event("Water Outage in Hatfield",
        "We regret to inform residents of a water outage due to urgent pipe repairs. Please plan accordingly, and\nwe apologize for any inconvenience caused.",
        "Hatfield, Pretoria",
        new DateTime(2024, 10, 13),
        new DateTime(2024, 10, 11, 12, 0, 0),
        "Utilities"),

    new Event("Road Work Notice",
        "Attention: There will be road work on Harry Galaun Road starting this week. Expect delays\nand plan alternative routes.",
        "Midrand",
        new DateTime(2024, 10, 17),
        new DateTime(2024, 10, 5, 15, 0, 0),
        "Roads"),

    new Event("Municipal Employment Fair",
        "Explore job opportunities with the municipality at our upcoming employment fair. Connect with recruiters\nand learn about available positions.",
        "Sandton",
        new DateTime(2024, 11, 5),
        new DateTime(2024, 10, 25, 13, 0, 0),
        "Other"),

    new Event("Community Cleanup",
        "Join us for a local community cleanup initiative aimed at beautifying our neighborhoods and\npromoting environmental awareness.",
        "Alexandra",
        new DateTime(2024, 10, 18),
        new DateTime(2024, 10, 12, 10, 0, 0),
        "Environmental Concerns"),

    new Event("Electricity Tariff Meeting",
        "Attend the public meeting regarding new electricity tariffs and how they will impact residents.\nYour feedback is important!",
        "Midrand",
        new DateTime(2024, 11, 3),
        new DateTime(2024, 10, 15, 12, 0, 0),
        "Utilities"),

    new Event("Flood Preparedness Drill",
        "Participate in a community drill focused on flood preparedness. Learn vital skills to keep your family\nsafe during emergencies.",
        "Vereeniging",
        new DateTime(2024, 10, 28),
        new DateTime(2024, 10, 15, 9, 0, 0),
        "Crime and Safety"),

    new Event("Youth Employment Programme",
        "Join us for the launch of a youth employment initiative aimed at helping young people secure meaningful\njob opportunities in the community.",
        "Soweto",
        new DateTime(2024, 10, 27),
        new DateTime(2024, 10, 19, 12, 0, 0),
        "Other"),

    new Event("Health and Wellness Fair",
        "Promoting health and wellness in communities through various activities, workshops, and health screenings.\nEveryone is welcome!",
        "Tembisa",
        new DateTime(2024, 10, 30),
        new DateTime(2024, 10, 20, 10, 0, 0),
        "Environmental Concerns"),

    new Event("Waste Management Summit",
        "Join local leaders and experts to discuss sustainable waste management practices and how we can\nimprove our community's environment.",
        "Pretoria",
        new DateTime(2024, 11, 10),
        new DateTime(2024, 10, 30, 9, 0, 0),
        "Sanitation"),

    new Event("Pothole Repair Notice",
        "Notice of pothole repairs in the city center. We appreciate your patience as we work to improve our roads.",
        "Johannesburg",
        new DateTime(2024, 10, 19),
        new DateTime(2024, 10, 12, 8, 0, 0),
        "Roads"),

    new Event("International Fire Safety Awareness Day",
        "Special events will be held today to promote fire safety awareness. Join us for informative sessions\nand community engagement activities.",
        "Pretoria",
        new DateTime(2024, 10, 6),
        new DateTime(2024, 10, 6, 9, 0, 0),
        "Crime and Safety"),

    new Event("Power Restoration Update",
        "Residents will receive updates on the power restoration efforts after the outage. Important information\nwill be shared during this session.",
        "Midrand",
        new DateTime(2024, 10, 12, 20, 0, 0),
        new DateTime(2024, 10, 10, 9, 0, 0),
        "Utilities"),

    new Event("Sewage Management Awareness",
        "A community event aimed at educating residents on sewage management and its importance for health\nand sanitation.",
        "Pretoria",
        new DateTime(2024, 10, 15),
        new DateTime(2024, 10, 8, 14, 0, 0),
        "Sanitation"),

    new Event("Community Safety Forum",
        "A forum where residents can discuss safety issues and concerns in their neighborhoods, and collaborate\nwith local authorities.",
        "Soweto",
        new DateTime(2024, 10, 20),
        new DateTime(2024, 10, 12, 11, 0, 0),
        "Crime and Safety"),

    new Event("Environmental Awareness Day",
        "Join us for a day of activities focused on environmental awareness, including workshops, cleanups\nand educational talks.",
        "Alexandra",
        new DateTime(2024, 10, 18),
        new DateTime(2024, 10, 12, 9, 0, 0),
        "Environmental Concerns")
};


            foreach (var ev in events)
            {
                eventQueue.Enqueue(ev); // Adding the event to the event queue
                priorityEvents.Add(ev);
                eventDetailsVisible[ev] = false; // // Initializing the visibility state of each event's details as false (details are hidden by default)
                categories.Add(ev.Category); // Adding the event's category to the HashSet to ensure unique categories are stored

                // Checking if there are any events already scheduled on the same date. If not, create a new list for that date.
                if (!eventsByDate.ContainsKey(ev.EventDate))
                {
                    eventsByDate[ev.EventDate] = new List<Event>();
                }
                // Adding the event to the list of events for the corresponding date. This allows grouping events by their date.
                eventsByDate[ev.EventDate].Add(ev);
            }
        }


        // This method was adapted by ChatGPT
        // Link: https://chatgpt.com
        // Method to add individual event cards
        private void AddEventCard(Event ev, ref int yOffset)
        {
            Panel eventPanel = new Panel // The panel for event data 
            {
                Size = new Size(700, 150),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray,
                Location = new Point(10, yOffset)
            };

            Label lblEventName = new Label // Name of the event 
            {
                Text = ev.Name,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            eventPanel.Controls.Add(lblEventName);

            Label lblEventDateLocationCategory = new Label // Date of the event underneath the event name
            {
                Text = $"{ev.EventDate.ToShortDateString()}",
                Font = new Font("Arial", 10),
                Location = new Point(10, 40),
                AutoSize = true
            };
            eventPanel.Controls.Add(lblEventDateLocationCategory);

            Label lblEventDetails = new Label // Displays the details for the event
            {
                Text = $"Description: {ev.Description}\nCategory: {ev.Category}\nLocation: {ev.Location}",
                Font = new Font("Arial", 9),
                AutoSize = true,
                Location = new Point(10, 70),
                Visible = eventDetailsVisible[ev]
            };
            eventPanel.Controls.Add(lblEventDetails);

            Button btnToggleDetails = new Button // Button to show more or less details of an event
            {
                Text = eventDetailsVisible[ev] ? "Show Less" : "View Details",
                Location = new Point(600, 10),
                Size = new Size(80, 30),
                Tag = ev
            };
            btnToggleDetails.Click += (sender, e) =>
            {
                ToggleEventDetails(ev, lblEventDetails, btnToggleDetails);
            };
            eventPanel.Controls.Add(btnToggleDetails);

            panelEvents.Controls.Add(eventPanel);
            yOffset += eventPanel.Height + 10;
        }

        // Method to show more or less event details by toggling a button
        private void ToggleEventDetails(Event ev, Label lblDetails, Button btnToggle)
        {
            eventDetailsVisible[ev] = !eventDetailsVisible[ev];
            lblDetails.Visible = eventDetailsVisible[ev];
            btnToggle.Text = eventDetailsVisible[ev] ? "Show Less" : "View Details";
        }


        // This method was adapted by ChatGPT
        // Link: https://chatgpt.com
        // Method to add the search UI
        private void AddSearchUI()
        {
            searchPanel = new Panel // Search panel that will contain all the search UI elements
            {
                Size = new Size(690, 60),
                Location = new Point(18, 10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
            };

            Label lblCategory = new Label
            {
                Text = "Category:",
                Font = new Font("Arial", 10),
                Location = new Point(10, 10),
                AutoSize = true
            };
            searchPanel.Controls.Add(lblCategory);

            cmbCategory = new ComboBox // Combo box to search for categories
            {
                Location = new Point(80, 8),
                Size = new Size(150, 25)
            };

            cmbCategory.Items.AddRange(uniqueCategories.ToArray()); // Adds the unique categories to the combo box
            cmbCategory.SelectedItem = "All Categories"; // Display All Categories by default
            searchPanel.Controls.Add(cmbCategory);

            Label lblDate = new Label
            {
                Text = "Date:",
                Font = new Font("Arial", 10),
                Location = new Point(250, 10),
                AutoSize = true
            };
            searchPanel.Controls.Add(lblDate);

            dtpDate = new DateTimePicker // Date picker for events 
            {
                Location = new Point(300, 8),
                Format = DateTimePickerFormat.Short,
                Size = new Size(150, 25)
            };
            searchPanel.Controls.Add(dtpDate);

            btnSearch = new Button // Button to search for events by category and date
            {
                Text = "Search",
                Location = new Point(480, 7),
                Size = new Size(150, 25)
            };
            btnSearch.Click += BtnSearch_Click;
            searchPanel.Controls.Add(btnSearch);

            this.Controls.Add(searchPanel);
        }

        // This method was adapted by ChatGPT
        // Link: https://chatgpt.com
        // Adding the recommendation panel
        private void AddRecommendationPanel()
        {
            recommendationPanel = new Panel // Panel to display recommended events 
            {
                Size = new Size(400, 600),
                Location = new Point(800, 100),
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle,
            };

            Label lblRecommendations = new Label
            {
                Text = "Recommended Events and Notices",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            recommendationPanel.Controls.Add(lblRecommendations);

            this.Controls.Add(recommendationPanel);
        }

        // This method was adpated from Simple Thread and modified using ChatGPT 
        // Simple Thread Link: https://www.simplethread.com/the-linq-selectmany-operator/
        // Author: Justin Etheredge
        // Method to display recommended events based on a specific category.
        private void DisplayRecommendations(string category)
        {
            // Clear any previous controls in the recommendation panel.
            recommendationPanel.Controls.Clear();

            Label lblRecommendations = new Label
            {
                Text = "Recommended Events and Notices",  
                Font = new Font("Arial", 12, FontStyle.Bold),  
                Location = new Point(10, 10),  
                AutoSize = true  
            };
            
            recommendationPanel.Controls.Add(lblRecommendations);

            int yOffset = 40;  

            // Filter and retrieve events that match the category or are upcoming events
            var relatedEvents = eventsByDate.Values
                .SelectMany(evList => evList)  
                .Where(ev => ev.Category == category || ev.EventDate >= DateTime.Now)  // Filter by category or upcoming events
                .OrderBy(ev => ev.EventDate) 
                .Take(5)  // Limit the number of recommendations to 5 events
                .ToList();  

            // Loop through each related event and display them as labels
            foreach (var ev in relatedEvents)
            {
                // Create and configure a label for each event, displaying its name and date.
                Label lblEvent = new Label
                {
                    Text = $"{ev.Name} ({ev.EventDate.ToShortDateString()})",  
                    Font = new Font("Arial", 10),  
                    Location = new Point(10, yOffset),  
                    AutoSize = true  
                };
                
                recommendationPanel.Controls.Add(lblEvent);
                yOffset += 30; 
            }
        }

        // This method was adapted by ChatGPT
        // Link: https://chatgpt.com
        // Method to handle search button click
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string selectedCategory = cmbCategory.SelectedItem?.ToString();
            DateTime selectedDate = dtpDate.Value.Date;

            // If the date hasn't been changed (still at DateTime.MinValue), disregard it for filtering
            bool isDateSelected = selectedDate != DateTime.MinValue;

            // Filter events based on selected category and date
            var filteredEvents = eventsByDate.Values
                .SelectMany(events => events)
                .Where(ev =>
                    (string.IsNullOrEmpty(selectedCategory) || selectedCategory == "All Categories" || ev.Category == selectedCategory) &&
                    (!isDateSelected || ev.EventDate.Date == selectedDate)) // Only filter by date if changed
                .ToList();

            // Clear previous event cards and display the filtered results
            panelEvents.Controls.Clear();
            int yOffset = 10;
            foreach (var ev in filteredEvents)
            {
                AddEventCard(ev, ref yOffset);
            }

            // Track search patterns for recommendations if a category is selected
            if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "All Categories")
            {
                if (searchPatterns.ContainsKey(selectedCategory))
                {
                    searchPatterns[selectedCategory]++;
                }
                else
                {
                    searchPatterns[selectedCategory] = 1;
                }
            }

            // Show related event recommendations
            DisplayRecommendations(selectedCategory);
        }

        // Method to display upcoming events using priority queue
        private void DisplayUpcomingEvents()
        {
            panelEvents.Controls.Clear();
            int yOffset = 10;

            foreach (var date in eventsByDate.Keys)
            {
                foreach (var ev in eventsByDate[date])
                {
                    AddEventCard(ev, ref yOffset);
                }
            }
        }
    }
}


// Event class to store event details
public class Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime DatePosted { get; set; }
        public string Category { get; set; }

        public Event(string name, string description, string location, DateTime eventDate, DateTime datePosted, string category)
        {
            Name = name;
            Description = description;
            Location = location;
            EventDate = eventDate;
            DatePosted = datePosted;
            Category = category;
        }
    }

    // Comparer for sorting events by date
    public class EventComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return x.EventDate.CompareTo(y.EventDate);
        }
    }

