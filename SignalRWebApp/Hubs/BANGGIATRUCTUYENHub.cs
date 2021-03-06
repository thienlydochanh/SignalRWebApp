using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRWebApp.SqlServerNotifier;

namespace SignalRWebApp.Hubs
{
    public class BANGGIATRUCTUYENHub : Hub
    {
        internal NotifierEntity NotifierEntity { get; private set; }

        public void DispatchToClient()
        {
            Clients.All.broadcastMessage("Refresh");
        }

        public void Initialize(String value)
        {
            NotifierEntity = NotifierEntity.FromJson(value);
            if (NotifierEntity == null)
                return;
            Action<String> dispatcher = (t) => { DispatchToClient(); };
            PushSqlDependency.Instance(NotifierEntity, dispatcher);
        }
    }
}