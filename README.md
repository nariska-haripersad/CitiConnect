# CitiConnect
CitiConnect is a Windows Forms application designed to improve how citizens engage with municipal services in South Africa. The project focuses on two core objectives: 
- Implementing advanced data structures (trees, heaps, graphs, queues, dictionaries, sets, etc.)
- Applying user engagement strategies (interactive UI elements, visual responsiveness, real-time feedback, etc.) and creating a clean, user-friendly interface (consistent layout, easy navigation, engaging visual indicators, etc.)
<p>The application allows users to report issues, view local events and announcements and monitor service request statuses. </p>

## Technologies Used 
- C#
- Windows Forms App (.NET Framework)
## Running the Application  
- Download the project folder
- Open in Visual Studio
- Run the solution
## Features 
#### Home Page 
When the application launches, users are welomed with the CitiConnect logo and a clean side navigation menu containing: 
- Home
- Report Issues
- Local Events and Announcements
- Service Request Status 
<p> All features can be accessed from the Home page or directly from the side menu - navigation is simple and intuitive. </p>

#### Report Issues 
An interactive page that lets users report community or municipal-related issues. </p>
Features: 
- Enter location of the issue
- Select issue category (Crime & Safety, Environmental Concerns, Roads, Sanitation, Utilities, Other)
- Enter description
- Attach images or documents <br>
— Clicking the upload area automatically opens File Explorer <br>
— Uploaded files appear as clickable preview attachments <br>
- Live progress bar <br>
— Fills as the user completes each section <br>
— Text updates from "Form Incomplete" (red) to "Form Complete" (green) <br>
- Submit the completed issue report
#### Local Events and Announcements
A dynamic page for browsing and exploring local events using multiple advanced data structures. 
- For each event, the event name, date and a 'View Details' button is displayed (clicking 'View Details' expands a section showing the event description, category and location - the button toggles to 'Show Less' to collapse)
- Users can filter events by category (via dropdown) and date (via date picker)
- Users can get personalized recommendations/view suggested events
###### Data structures such as Queue, SortedSet, SortedDictionary, HashSet and Dictionary are used to store events, track search patterns and recommend relevant events based on user activity.
#### Service Request Status
An interactive page for viewing and tracking service request submissions. <br>
Each request shows the: 
- Request ID
- Name
- Description
- Location
- Category
- Date
- Status badge <br>
— Submitted (red) <br>
— In-Process (blue) <br>
— Completed (green) <br>

Other features: 
- Dynamic search bar (results update instantly as the user types)
- Filter by status
- A hierarchical (tree-view) structure where users can browse service requests by location and expand/collapse items to view details






  
