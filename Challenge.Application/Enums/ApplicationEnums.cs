using System.ComponentModel;

namespace Challenge.Application.Enums {
    public class ApplicationEnums {

        public enum Operation {
            [Description("get")]
            Get,
            [Description("request")]
            Request,
            [Description("modify")]
            Modify
        }

    }
}
