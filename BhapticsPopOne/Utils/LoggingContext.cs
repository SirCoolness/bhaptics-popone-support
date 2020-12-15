using System;
using System.Collections.Generic;
using System.Linq;
using PlayFab.Internal;

namespace BhapticsPopOne
{
    public class LoggingContext
    {
        private string _label;

        public string Label => _label;

        private LoggingContext _parent;

        public LoggingContext Parent => _parent;

        public string Prefix
        {
            get
            {
                var higherarchy = Higherarchy;

                return "[" + String.Join("] [", higherarchy.Select(x => x.Label)) + "]";
            }
        }

        public List<LoggingContext> Higherarchy
        {
            get
            {
                if (_parent == this)
                {
                    var rootResult = new List<LoggingContext>();
                    rootResult.Add(this);
                    return rootResult;
                }
                
                var result = _parent.Higherarchy;

                result.Add(this);
                
                return result;
            }
        }

        public LoggingContext(string label)
        {
            this._label = label;
            this._parent = this;
        }

        public LoggingContext(string label, LoggingContext parent)
        {
            this._label = label;
            this._parent = parent;
        }
        
    }
}