namespace POP_SF7
{
    class Language
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public bool Deleted { get; set; }

        public Language(int languageId, string languageName, bool deleted)
        {
            Id = languageId;
            LanguageName = languageName;
            Deleted = deleted;
        }
    }
}
