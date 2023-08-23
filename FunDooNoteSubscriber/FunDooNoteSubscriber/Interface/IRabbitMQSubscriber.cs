using System;
using System.Collections.Generic;
using System.Text;

namespace FunDooNoteSubscriber.Interface
{
    public interface IRabbitMQSubscriber
    {
        void ConsumeMessages();
    }
}
