﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.Core.Exceptions
{
    namespace SwissTournament.Core.Exceptions
    {
        [Serializable]
        public class UpdateEntityException : Exception
        {
            public UpdateEntityException() { }
            public UpdateEntityException(string entityName) : base("There was an error updating that " + entityName) { }
            public UpdateEntityException(string entityName, Exception inner) : base("There was an error updating that " + entityName, inner) { }
            protected UpdateEntityException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}