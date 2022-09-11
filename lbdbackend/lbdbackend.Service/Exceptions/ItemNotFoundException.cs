using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.Exceptions {
    public class ItemNotFoundException : Exception {
        public ItemNotFoundException() {
        }

        public ItemNotFoundException(string msg) : base(msg) {

        }
    }
}
