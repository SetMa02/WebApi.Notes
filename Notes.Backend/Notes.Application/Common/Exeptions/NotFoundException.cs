using System;

namespace Notes.Application.Common.Exeptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) not found")
        {
            
        }
      
    }
}