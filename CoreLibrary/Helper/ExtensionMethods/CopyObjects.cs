using System.Reflection;

namespace CoreLibrary.Helper.ExtensionMethods
{
    public static class CopyObjects
    {

        public static void CopyToMeNoId<T, U>(this T receiver, U sender) {
            CopyToMeExcludeList(receiver, sender, "Id");
        }
        public static void CopyToMeExcludeList<T, U>(this T receiver, U sender, params string[] noCopyList) {
            Func<string, bool> skipProperty = x => noCopyList.Contains(x, StringComparer.OrdinalIgnoreCase);
            CopyToMe(receiver, sender, skipProperty);
        }
        public static void CopyToMe<T, U>(this T receiver, U sender) {
            CopyToMe(receiver, sender, x => false);
        }
        private static void CopyToMe<T, U>(this T receiver, U sender, Func<string, bool> skipProperty) {
            Type receiverType = receiver.GetType();
            Type senderType = sender.GetType();

            PropertyInfo[] senderProperties = senderType.GetProperties();

            foreach (PropertyInfo senderProperty in senderProperties) {
                if (skipProperty.Invoke(senderProperty.Name))
                    continue;

                if (senderProperty.CanRead && senderProperty.CanWrite) {
                    PropertyInfo receiverProperty = receiverType.GetProperty(senderProperty.Name);

                    if (receiverProperty != null && receiverProperty.PropertyType == senderProperty.PropertyType) {
                        receiverProperty.SetValue(receiver, senderProperty.GetValue(sender));
                    }
                }
            }

        }
    }
}
