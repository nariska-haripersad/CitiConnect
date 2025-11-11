using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiConnect
{
    // This code was adapted from Medium
    // Name of the Website: Graphs in C#
    // Author: M.F.M Fazrin
    // Date Published: 10 December 2022
    // Link: https://mfmfazrin.medium.com/directed-undirected-nodes-a4facce605fb
    // Link to Author's Page: https://mfmfazrin.medium.com/

    // Graph class to represent the status of service requests
    public class Graph
    {
        // Dictionary to store nodes categorized by their status
        public Dictionary<string, List<ServiceRequestNode>> StatusGraph;

        // Constructor to initialize the graph with predefined statuses
        public Graph()
        {
            StatusGraph = new Dictionary<string, List<ServiceRequestNode>>()
                {
                    { "Submitted", new List<ServiceRequestNode>() },
                    { "In-Process", new List<ServiceRequestNode>() },
                    { "Completed", new List<ServiceRequestNode>() },
                    { "All", new List<ServiceRequestNode>() } // "All" to store all nodes regardless of status
                };
        }

        // Add a service request node to the appropriate status list
        public void AddNode(ServiceRequestNode node)
        {
            if (StatusGraph.ContainsKey(node.Status))
            {
                StatusGraph[node.Status].Add(node); // Add to the specific status list
                StatusGraph["All"].Add(node); // Add to the "All" list to keep a complete record
            }
        }

        // Retrieve service requests filtered by status
        public List<ServiceRequestNode> GetFilteredRequests(string status)
        {
            // Return the list for the given status
            return StatusGraph.ContainsKey(status) ? StatusGraph[status] : new List<ServiceRequestNode>();
        }
    }
}
