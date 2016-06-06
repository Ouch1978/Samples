using System.ComponentModel.DataAnnotations;

namespace SL_CustomValidation
{
    public class CustomValidation
    {
        private string _message;

        public CustomValidation( string message )
        {
            this._message = message;
        }

        public bool ShowErrorMessage
        {
            get;
            set;
        }

        public object ValidationError
        {
            get
            {
                return null;
            }
            set
            {
                if( ShowErrorMessage )
                {
                    throw new ValidationException( _message );
                }
            }
        }
    }
}
