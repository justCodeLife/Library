namespace Library.PublicModel
{
    public class ModalFooter
    {
        public string SubmitButtonID { get; set; } = "btn-submit";
        public string CancelButtonID { get; set; } = "btn-cancel";
        public string SubmitButtonText { get; set; } = "Submit";
        public string CancelButtonText { get; set; } = "برگشت";
        public bool OnlyCancelButton { get; set; }
    }
}