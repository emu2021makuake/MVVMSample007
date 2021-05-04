namespace MVVMSample007.Models
{
    public class ProgressInfo
    {
        public int ProgressValue { get; set; }
        public string ProgressText { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="text"></param>
        public ProgressInfo(int value, string text)
        {
            ProgressValue = value;
            ProgressText = text;
        }
    }
}
