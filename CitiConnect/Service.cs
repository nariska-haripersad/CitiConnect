using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static CitiConnect.Service;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace CitiConnect
{
    public partial class Service : Form
    {
        // To initialize the data structures used
        private AVLTree serviceRequests;
        private MinHeap minHeap;
        private List<ServiceRequestNode> allRequests;
        private Graph serviceRequestGraph;
        private int itemCount = 0;
        

        public Service()
        {
            InitializeComponent();
            serviceRequests = new AVLTree(); // To store and balance service requests
            minHeap = new MinHeap(); // To manage requests by priority
            serviceRequestGraph = new Graph(); // For filtering and organizing requests
            allRequests = new List<ServiceRequestNode>(); // List to store all service requests
            InitializeServiceRequests(); // To create and insert sample service requests
            PopulateRequests(); // To populate the UI panel with the service requests
            CreateProvinceButtons(); // Create buttons for different provinces for filtering purposes
            InitializeComboBox(); // To allow filtering by request status
        }

        private void InitializeServiceRequests()
        {
            var requests = new List<ServiceRequestNode>
            
            {
                // These service requests were adapted from the Education and Training Unit For Democracy & Development
                // Link: https://etu.org.za/guides/municipal-service-delivery/#:~:text=Municipalities%20must%20make%20sure%20that,Refuse%20removal
                // Sample service requests 
                new ServiceRequestNode(101, "Water Supply", "Water outage in the area affecting several households, with no estimated restoration time provided.", "Utilities", "Durban North, KwaZulu-Natal", "Submitted", new DateTime(2024, 11, 1, 7, 30, 0)),
                new ServiceRequestNode(102, "Sewage Collection", "Sewage backup near residential homes causing unpleasant odors and potential health hazards.", "Sanitation", "Bloemfontein, Free State", "In-Process", new DateTime(2024, 11, 2, 14, 45, 0)),
                new ServiceRequestNode(103, "Refuse Removal", "Missed garbage collection for the past two days, resulting in waste accumulation on the street.", "Sanitation", "Randburg, Johannesburg", "Completed", new DateTime(2024, 11, 3, 9, 00, 0)),
                new ServiceRequestNode(104, "Electricity Supply", "A power outage has been reported, affecting local businesses and residential areas, causing inconvenience.", "Utilities", "Polokwane, Limpopo", "In-Process", new DateTime(2024, 11, 4, 16, 00, 0)),
                new ServiceRequestNode(105, "Road Repair", "Potholes on the main road have been causing accidents and vehicle damage. Urgent repair needed.", "Roads", "Port Elizabeth, Eastern Cape", "Submitted", new DateTime(2024, 11, 5, 11, 10, 0)),
                new ServiceRequestNode(106, "Park Maintenance", "Broken equipment in the local park, creating a safety risk for children and park visitors.", "Environmental Concerns", "Nelspruit, Mpumalanga", "Submitted", new DateTime(2024, 11, 6, 12, 30, 0)),
                new ServiceRequestNode(107, "Traffic Lights", "Traffic lights malfunctioning at the main intersection, causing traffic congestion and safety concerns.", "Roads", "Vanderbijlpark, Gauteng", "In-Process", new DateTime(2024, 11, 7, 8, 20, 0)),
                new ServiceRequestNode(108, "Street Cleaning", "Street cleaning overdue, resulting in litter build-up and unsanitary conditions on the sidewalk.", "Sanitation", "Mossel Bay, Western Cape", "Completed", new DateTime(2024, 11, 8, 17, 00, 0)),
                new ServiceRequestNode(109, "Noise Complaint", "Loud music at night disturbing the neighborhood, affecting the quality of life for residents.", "Crime & Safety", "East London, Eastern Cape", "Submitted", new DateTime(2024, 11, 9, 22, 15, 0)),
                new ServiceRequestNode(110, "Graffiti Removal", "Offensive graffiti spray-painted on public walls near a school, posing a threat to public safety.", "Other", "Kimberley, Northern Cape", "Completed", new DateTime(2024, 11, 10, 14, 50, 0)),
                new ServiceRequestNode(111, "Stormwater Drainage", "Blocked stormwater drains causing flooding in the area, affecting both homes and businesses.", "Utilities", "North Beach, Durban", "In-Process", new DateTime(2024, 11, 2, 8, 15, 0)),
                new ServiceRequestNode(112, "Building Inspection", "Request for urgent building inspection due to potential structural issues after recent tremors.", "Other", "Pietermaritzburg, KwaZulu-Natal", "Submitted", new DateTime(2024, 11, 3, 10, 30, 0)),
                new ServiceRequestNode(113, "Pedestrian Crossing", "Request for a new pedestrian crossing at a busy intersection to improve safety for pedestrians.", "Roads", "Sandton, Johannesburg", "In-Process", new DateTime(2024, 11, 4, 14, 00, 0)),
                new ServiceRequestNode(114, "Noise Complaint", "Loud generator noise disturbing the neighborhood, especially late at night.", "Crime & Safety", "Pretoria West, Pretoria", "Submitted", new DateTime(2024, 11, 5, 21, 10, 0)),
                new ServiceRequestNode(115, "Potholes", "Numerous large potholes on the main road causing vehicle damage and traffic disruptions.", "Roads", "George, Western Cape", "In-Process", new DateTime(2024, 11, 6, 9, 30, 0)),
                new ServiceRequestNode(116, "Street Lighting", "Streetlights out along the main road, making it unsafe for pedestrians and motorists after dark.", "Utilities", "Mthatha, Eastern Cape", "Submitted", new DateTime(2024, 11, 7, 18, 50, 0)),
                new ServiceRequestNode(117, "Fallen Trees", "Several large trees have fallen over the sidewalk, blocking pedestrian traffic and causing a safety hazard.", "Environmental Concerns", "Stellenbosch, Western Cape", "In-Process", new DateTime(2024, 11, 8, 16, 00, 0)),
                new ServiceRequestNode(118, "Vandalism Report", "Public bench has been destroyed by vandals, leaving the park area unsightly and unusable.", "Crime & Safety", "Polokwane, Limpopo", "Completed", new DateTime(2024, 11, 9, 11, 45, 0)),
                new ServiceRequestNode(119, "Public Transport", "Request for a new public transport bus stop at the central area to cater to growing demand.", "Other", "Port Elizabeth, Eastern Cape", "Submitted", new DateTime(2024, 11, 10, 13, 00, 0)),
                new ServiceRequestNode(120, "Graffiti Removal", "Request for removal of offensive graffiti from public walls to improve the neighborhood's appearance.", "Other", "Cape Town, Western Cape", "In-Process", new DateTime(2024, 11, 11, 15, 25, 0))
            };

            // Insert each service request into the AVL Tree, MinHeap and Graph
            foreach (var request in requests)
            {
                serviceRequests.Root = serviceRequests.Insert(serviceRequests.Root, request);
                minHeap.Add(request);
                serviceRequestGraph.AddNode(request); // Add each request to the Graph
                allRequests.Add(request);
            }
        }

        // Method to populate a panel with service requests
        private void PopulateRequests()
        {
            panelServiceRequests.Controls.Clear(); // Clear any existing controls on the panel
            itemCount = 0; // Reset the item count

            // Get filtered requests from the Graph and display each one
            foreach (var request in serviceRequestGraph.GetFilteredRequests("All"))
            {
                DisplayRequest(request); // Display each request in the panel
            }

            // Update the label to show the total number of items displayed
            lblItemCount.Text = $"Items: {itemCount}";
        }

        // Initialize the combo box with status options
        private void InitializeComboBox()
        {
            comboBoxLayout.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLayout.Items.Add("All");
            comboBoxLayout.Items.Add("Submitted");
            comboBoxLayout.Items.Add("In-Process");
            comboBoxLayout.Items.Add("Completed");
            comboBoxLayout.SelectedIndex = 0; // Default to 'All'
           
        }

        // This code was adapted from GeeksforGeeks
        // Website Name: Tree Traversal Techniques
        // Link: https://www.geeksforgeeks.org/tree-traversals-inorder-preorder-and-postorder/
        // Date: 31 July 2024
        private void InOrderTraversal(ServiceRequestNode node)
        {
            if (node == null) return;

            InOrderTraversal(node.Left);
            DisplayRequest(node);
            InOrderTraversal(node.Right);
        }

        // This method was adapted by ChatGPT
        // Link: https://chatgpt.com
        private void DisplayRequest(ServiceRequestNode node)  // Method to display a service requests
        {
            // Create a panel for each service request
            var requestPanel = new Panel
            {
                Width = 400, 
                Height = 190, 
                BackColor = Color.White, 
                BorderStyle = BorderStyle.None, 
                Margin = new Padding(10), 
            };

            // Label to display the ID of the service request
            Label idLabel = new Label
            {
                Text = "ID: " + node.Id.ToString(),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(requestPanel.Width - 120, 10), 
                TextAlign = ContentAlignment.TopRight
            };

            // Label to display the name of the service request
            var lblName = new Label
            {
                Text = node.Name,
                Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold),
                Location = new System.Drawing.Point(10, 10),
                Width = 270
            };

            // Label to display the description of the service request
            Label descriptionLabel = new Label
            {
                Text = node.Description,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Location = new Point(10, 50),
                Width = requestPanel.Width - 20,
                TextAlign = ContentAlignment.TopLeft,
                AutoSize = false, 
                MaximumSize = new Size(requestPanel.Width - 20, 0), 
                Height = 0 
            };
            descriptionLabel.Height = TextRenderer.MeasureText(descriptionLabel.Text, descriptionLabel.Font, new Size(descriptionLabel.Width, int.MaxValue), TextFormatFlags.WordBreak).Height;

            // To display the service request status
            var lblStatus = new Label
            {
                Text = node.Status,
                Font = new System.Drawing.Font("Segoe UI", 9),
                Location = new System.Drawing.Point(140, 155),
                Width = 120,
                BackColor = GetStatusColor(node.Status),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Label to display the category of a service request 
            var lblCategory = new Label
            {
                Text = "Category: " + node.Category,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Location = new Point(0, 105),
                Width = requestPanel.Width,
                TextAlign = ContentAlignment.MiddleCenter

            };

            // Label to display the location of a service request
            var lblPlace = new Label
            {
                Text = node.Location.Split(',')[0],
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Location = new Point(0, 85),
                Width = requestPanel.Width,
                TextAlign = ContentAlignment.MiddleCenter

            };

            // Label to display the date of a service request
            var lblDate = new Label
            {
                Text = node.RequestDate.ToString("yyyy-MM-dd"),
                Font = new Font("Segoe UI", 7, FontStyle.Regular),
                Location = new Point(0, 130),
                Width = requestPanel.Width,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Add labels to the request panel
            requestPanel.Controls.Add(idLabel);
            requestPanel.Controls.Add(lblName);
            requestPanel.Controls.Add(descriptionLabel);
            requestPanel.Controls.Add(lblPlace);
            requestPanel.Controls.Add(lblCategory);
            requestPanel.Controls.Add(lblDate);
            requestPanel.Controls.Add(lblStatus);

            panelServiceRequests.Controls.Add(requestPanel);
            itemCount++; // Increment the item count
        }

        // Method to get the background color based on the status of the service request
        private Color GetStatusColor(string status)
        {
            switch (status)
            {
                case "Submitted":
                    return Color.LightPink;
                case "In-Process":
                    return Color.LightBlue;
                case "Completed":
                    return Color.LightGreen;
                default:
                    return Color.Gray;
            }
        }

        // This code was adapted from Stack Overflow and modified using ChatGPT 
        // Stack Overflow Link: https://stackoverflow.com/questions/49814585/searching-through-objects-in-binary-tree-with-string-parameters-and-generic-type
        // Author: Prime
        // ChatGPT Link: https://chatgpt.com
        private void SearchServiceRequests(ServiceRequestNode node, string query, List<ServiceRequestNode> results)
        {
            if (node == null) return;

            // Check if any field matches the search query
            if (node.Name.ToLower().Contains(query) ||
                node.Description.ToLower().Contains(query) ||
                node.Category.ToLower().Contains(query) ||
                node.Location.ToLower().Contains(query) ||
                node.Status.ToLower().Contains(query))
            {
                results.Add(node);  // Add matching node to results
            }

            // Continue searching in both left and right subtrees
            SearchServiceRequests(node.Left, query, results);
            SearchServiceRequests(node.Right, query, results);
        }

        // This method was adapted by ChatGPT
        // Link: https://chatgpt.com
        private void DisplaySearchResults(List<ServiceRequestNode> results) // To display the service requests from a search
        {
            panelServiceRequests.Controls.Clear();
            foreach (var request in results)
            {

                var requestPanel = new Panel
                {
                    Width = 400, 
                    Height = 190, 
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    Margin = new Padding(10), 
                };

                Label idLabel = new Label
                {
                    Text = "ID: " + request.Id.ToString(),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(requestPanel.Width - 120, 10), 
                    TextAlign = ContentAlignment.TopRight
                };


                var lblName = new Label
                {
                    Text = request.Name,
                    Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new System.Drawing.Point(10, 10),
                    Width = 270
                };

                Label descriptionLabel = new Label
                {
                    Text = request.Description,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Location = new Point(10, 50),
                    Width = requestPanel.Width - 20, 
                    TextAlign = ContentAlignment.TopLeft,
                    AutoSize = false, 
                    MaximumSize = new Size(requestPanel.Width - 20, 0), 
                    Height = 0 
                };

                descriptionLabel.Height = TextRenderer.MeasureText(descriptionLabel.Text, descriptionLabel.Font, new Size(descriptionLabel.Width, int.MaxValue), TextFormatFlags.WordBreak).Height;

                var lblStatus = new Label
                {
                    Text = request.Status,
                    Font = new System.Drawing.Font("Segoe UI", 9),
                    Location = new System.Drawing.Point(140, 155),
                    Width = 120,
                    BackColor = GetStatusColor(request.Status),
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                var lblCategory = new Label
                {
                    Text = "Category: " + request.Category,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Location = new Point(0, 105),
                    Width = requestPanel.Width,
                    TextAlign = ContentAlignment.MiddleCenter

                };
                var lblPlace = new Label
                {
                    Text = request.Location.Split(',')[0],
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Location = new Point(0, 85),
                    Width = requestPanel.Width,
                    TextAlign = ContentAlignment.MiddleCenter

                };
                var lblDate = new Label
                {
                    Text = request.RequestDate.ToString("yyyy-MM-dd"),
                    Font = new Font("Segoe UI", 7, FontStyle.Regular),
                    Location = new Point(0, 130),
                    Width = requestPanel.Width,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                requestPanel.Controls.Add(idLabel);
                requestPanel.Controls.Add(lblName);
                requestPanel.Controls.Add(descriptionLabel);
                requestPanel.Controls.Add(lblPlace);
                requestPanel.Controls.Add(lblCategory);
                requestPanel.Controls.Add(lblDate);
                requestPanel.Controls.Add(lblStatus);


                panelServiceRequests.Controls.Add(requestPanel);
            }
            lblItemCount.Text = $"Items: {results.Count}";  // Update item count based on search results
        }

        // Searches for requests based on the status that the user selected
        private void PredictiveFilteredSearch(string query)
        {
            var filteredResults = new List<ServiceRequestNode>();
            string selectedStatus = comboBoxLayout.SelectedItem.ToString();

            // Filter based on the selected status
            if (selectedStatus != "All")
            {
                SearchServiceRequests(serviceRequests.Root, query.ToLower(), filteredResults);
                filteredResults = filteredResults.Where(request => request.Status == selectedStatus).ToList();
            }
            else
            {
                // If status is "All", just filter by query
                SearchServiceRequests(serviceRequests.Root, query.ToLower(), filteredResults);
            }

            // Sort the filtered results by ID before displaying
            filteredResults = filteredResults.OrderBy(request => request.Id).ToList();

            DisplaySearchResults(filteredResults);
        }

        // For when the user enteres text in the Search box 
        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {

            string query = txtSearch.Text.ToLower();
            if (string.IsNullOrWhiteSpace(query) || query == "search...")
            {
                PredictiveFilteredSearch(query);
            }
            else
            {
                
                PredictiveFilteredSearch(query);
            }


        }

        // For the user to select or filter service requests by status 
        private void comboBoxLayout_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedStatus = comboBoxLayout.SelectedItem.ToString();
            panelServiceRequests.Controls.Clear();
            itemCount = 0;
            foreach (var request in serviceRequestGraph.GetFilteredRequests(selectedStatus))
            {
                DisplayRequest(request);
            }
            lblItemCount.Text = $"Items: {itemCount}";
        }

        // To create the buttons for the province options
        private void CreateProvinceButtons()
        {
            // List of provinces
            string[] provinces = { "Gauteng", "KwaZulu-Natal", "Western Cape", "Eastern Cape", "Free State", "Limpopo", "Mpumalanga", "Northern Cape", "North West" };

            int x = 10;
            int y = 10;

            for (int i = 0; i < provinces.Length; i++)
            {
                string province = provinces[i]; // Capture the current province in a local variable
                var button = new Button
                {
                    Text = province,
                    Location = new Point(x, y),
                    Size = new Size(150, 50), 
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    BackColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };

                button.FlatAppearance.BorderSize = 0;
                
                var path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, 150, button.Height);
                button.Region = new Region(path);

                button.Click += (s, e) => ProvinceButton_Click(s, e, province);
                panelProvinces.Controls.Add(button);

                y += button.Height + 10; 
            }
        }

        // When the user clicks on one of the provinces 
        private void ProvinceButton_Click(object sender, EventArgs e, string province)
        {
            var button = sender as Button;
            if (button == null || panelProvinces == null) return;

            try
            {
                foreach (Control ctrl in panelProvinces.Controls)
                {
                    if (ctrl is Button btn)
                    {
                        btn.BackColor = ControlPaint.Light(btn.BackColor);
                    }
                }

                //If there's no service requests for that province 
                if (allRequests == null)
                {
                    MessageBox.Show("No requests to display.");
                    return;
                }

                // Filter requests for the selected province
                var filteredRequests = allRequests
                    .Where(r => r.Location != null && r.Location.Contains(province))
                    .ToList();

                // Clear and populate the TreeView with service request details based on the selected province
                treeViewServiceRequests.Nodes.Clear();
                foreach (var request in filteredRequests)
                {
                    TreeNode requestNode = new TreeNode($"ID: {request.Id}");
                    requestNode.Nodes.Add($"Name: {request.Name}");
                    requestNode.Nodes.Add($"Location: {request.Location}");
                    requestNode.Nodes.Add($"Date: {request.RequestDate.ToString("g")}");
                    requestNode.Nodes.Add($"Status: {request.Status}");
                    treeViewServiceRequests.Nodes.Add(requestNode);
                }

                if (filteredRequests.Count == 0)
                {
                    MessageBox.Show("No requests found for the selected province.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

    } 

}
