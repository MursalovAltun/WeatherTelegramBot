using System.Runtime.Serialization;

namespace Common.DTO
{
    public class UserDTO
    {
        /// <summary>
        /// Unique identifier for this user or bot
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// True, if this user is a bot
        /// </summary>
        [DataMember(Name = "is_bot")]
        public bool IsBot { get; set; }

        /// <summary>
        /// User‘s or bot’s first name
        /// </summary>
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// User‘s or bot’s last name
        /// </summary>
        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// User‘s or bot’s username
        /// </summary>
        [DataMember(Name = "username")]
        public string UserName { get; set; }

        /// <summary>
        /// Optional.
        /// True, if the bot can be invited to groups. Returned only in getMe.
        /// </summary>
        [DataMember(Name = "language_code")]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Optional.
        /// True, if privacy mode is disabled for the bot. Returned only in getMe.
        /// </summary>
        [DataMember(Name = "can_join_groups")]
        public bool CanJoinGroups { get; set; }

        /// <summary>
        /// Optional.
        /// True, if privacy mode is disabled for the bot. Returned only in getMe.
        /// </summary>
        [DataMember(Name = "can_read_all_group_messages")]
        public bool CanReadAllGroupMessages { get; set; }

        /// <summary>
        /// Optional.
        /// True, if the bot supports inline queries. Returned only in getMe.
        /// </summary>
        [DataMember(Name = "supports_inline_queries")]
        public bool SupportsInlineQueries { get; set; }
    }

    public class RootUser
    {
        [DataMember(Name = "ok")]
        public bool Ok { get; set; }

        [DataMember(Name = "result")]
        public UserDTO Result { get; set; }
    }
}