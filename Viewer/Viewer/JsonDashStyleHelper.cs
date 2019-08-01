using System.Windows.Media;

namespace Viewer
{
    public static class JsonDashStyleHelper
    {
        private const string Dash = "dash";
        private const string Dot = "dot";
        private const string DashDot = "dashDot";
        private const string Solid = "solid";

        public static string ToJson(DashStyle dashStyle)
        {
            if (dashStyle == DashStyles.DashDot)
                return DashDot;

            if (dashStyle == DashStyles.Dash)
                return Dash;

            if (dashStyle == DashStyles.Dot)
                return Dot;

            if (dashStyle == DashStyles.Solid)
                return Solid;

            return Solid;
        }

        public static DashStyle FromJson(string dashStyle)
        {
            if (dashStyle == DashDot)
                return DashStyles.DashDot;

            if (dashStyle == Dash)
                return DashStyles.Dash;

            if (dashStyle == Dot)
                return DashStyles.Dot;

            if (dashStyle == Solid)
                return DashStyles.Solid;

            return DashStyles.Solid;
        }


    }
}