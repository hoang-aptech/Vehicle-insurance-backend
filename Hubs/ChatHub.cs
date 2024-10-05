using Microsoft.AspNetCore.SignalR;
using vehicle_insurance_backend.DataCtxt;
using vehicle_insurance_backend.models;

namespace vehicle_insurance_backend.Hubs
{
    public class ChatHub : Hub
    {
        private readonly DataContext _context;


        public ChatHub(DataContext context)
        {
            _context = context;
        }
        public async Task JoinSpecificGroup(int customerSupportId)
        {
            // Add the current connection to the specified group
            await Groups.AddToGroupAsync(Context.ConnectionId, customerSupportId.ToString());

            // Query the messages related to this customer support
            var messages = _context.messages
                .Where(m => m.customersupportId == customerSupportId)
                .OrderBy(m => m.time)
                .ToList();

            // Send the queried messages to the client that joined the group
            await Clients.Caller.SendAsync("ReceiveMessages", messages);
        }
        public async Task SendMessage(Message m)
        {
            // Add the message to the database (if applicable)
            _context.messages.Add(m);
            await _context.SaveChangesAsync();

            // Send the new message to all clients in the group
            await Clients.Group(m.customersupportId.ToString()).SendAsync("ReceiveMessage", m);
        }
    }
}
