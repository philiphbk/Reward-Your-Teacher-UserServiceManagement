using System.ComponentModel;

namespace RYTUserManagementService.Common.Utilities
{
    public class Constants
    {
        public const string MobileNumberRegex = @"^((234)(\d{10}))|([0]?(\d{10}))$";
        public const int Max2000Length = 2000;
        public const int Max15Length = 15;
        
        public const int Max100Length = 100;
        public const int Max200Length = 200;


        public enum Titles
        {
            [Description("Mr.")]
            Mr = 0,

            [Description("Mrs.")]
            Mrs,
            [Description("Miss. ")]
            Miss,
            [Description("Dr. ")]
            Dr,
            [Description("Prof. ")]
            Prof

        } 
        
        public enum SchoolType
        {
            [Description("Primary School")]
            Primary = 0,

            [Description("Secondary School")]
            Secondary,
        }

    }
}