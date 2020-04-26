using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OSConsole
{
    public class CommincaitonHub : Hub<ICommunicationHub>
    {
        
    }

    public interface ICommunicationHub
    {
        Task Communicate();
    }
}
