using System;
using System.Collections.Generic;
using System.Text;

namespace P225NLayerArchitectura.Service.Exceptions {
    public class AlreadyExistException : Exception {
        public AlreadyExistException(string msg) : base(msg) {

        }
    }
}
