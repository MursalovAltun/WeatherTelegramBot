using System.Runtime.Serialization;

namespace Common.DTO
{
    /// <summary>
    /// This object represents one special entity in a text message.
    /// For example, hashtags, usernames, URLs, etc.
    /// </summary>
    public class MessageEntityDTO
    {
        /// <summary>
        /// Type of the entity.
        /// Can be “mention” (@username), “hashtag” (#hashtag), “cashtag” ($USD),
        /// “bot_command” (/start@jobs_bot), “url” (https://telegram.org), “email” (do-not-reply@telegram.org),
        /// “phone_number” (+1-212-555-0123), “bold” (bold text), “italic” (italic text),
        /// “underline” (underlined text), “strikethrough” (strikethrough text), “code” (monowidth string),
        /// “pre” (monowidth block), “text_link” (for clickable text URLs),
        /// “text_mention” (for users without usernames)
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Offset in UTF-16 code units to the start of the entity
        /// </summary>
        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Length of the entity in UTF-16 code units
        /// </summary>
        [DataMember(Name = "length")]
        public int Length { get; set; }

        /// <summary>
        /// Optional
        /// For “text_link” only, url that will be opened after user taps on the text
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Optional
        /// For “text_mention” only, the mentioned user
        /// </summary>
        [DataMember(Name = "user")]
        public UserDTO User { get; set; }

        /// <summary>
        /// Optional
        /// For “pre” only, the programming language of the entity text
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }
    }
}