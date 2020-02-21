using System.Runtime.Serialization;

namespace Common.DTO
{
    /// <summary>
    /// This object represents a chat.
    /// </summary>
    public class ChatDTO
    {
        /// <summary>
        /// Unique identifier for this chat.
        /// This number may be greater than 32 bits and some programming languages may have
        /// difficulty/silent defects in interpreting it. But it is smaller than 52 bits,
        /// so a signed 64 bit integer or double-precision float type are safe for storing this identifier.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Type of chat, can be either “private”, “group”, “supergroup” or “channel”
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Optional
        /// Title, for supergroups, channels and group chats
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Optional
        /// Username, for private chats, supergroups and channels if available
        /// </summary>
        [DataMember(Name = "username")]
        public string UserName { get; set; }

        /// <summary>
        /// Optional
        /// First name of the other party in a private chat
        /// </summary>
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Optional
        /// Last name of the other party in a private chat
        /// </summary>
        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Optional
        /// Chat photo. Returned only in getChat
        /// </summary>
        [DataMember(Name = "photo")]
        public ChatPhotoDTO Photo { get; set; }

        /// <summary>
        /// Optional
        /// Description, for groups, supergroups and channel chats.
        /// Returned only in getChat.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Optional
        /// Chat invite link, for groups, supergroups and channel chats.
        /// Each administrator in a chat generates their own invite links,
        /// so the bot must first generate the link using exportChatInviteLink.
        /// Returned only in getChat.
        /// </summary>
        [DataMember(Name = "invite_link")]
        public string InviteLink { get; set; }

        /// <summary>
        /// Optional
        /// Pinned message, for groups, supergroups and channels.
        /// Returned only in getChat.
        /// </summary>
        [DataMember(Name = "pinned_message")]
        public MessageDTO PinnedMessage { get; set; }

        /// <summary>
        /// Optional
        /// Default chat member permissions, for groups and supergroups.
        /// Returned only in getChat.
        /// </summary>
        [DataMember(Name = "permissions")]
        public CharPermisstionsDTO Permissions { get; set; }

        /// <summary>
        /// Optional
        /// For supergroups, the minimum allowed delay between consecutive messages
        /// sent by each unpriviledged user.
        /// Returned only in getChat.
        /// </summary>
        [DataMember(Name = "slow_mode_delay")]
        public int SlowModeDelay { get; set; }

        /// <summary>
        /// Optional
        /// For supergroups, name of group sticker set.
        /// Returned only in getChat.
        /// </summary>
        [DataMember(Name = "sticker_set_name")]
        public string StickerSetName { get; set; }

        /// <summary>
        /// Optional
        /// True, if the bot can change the group sticker set.
        /// Returned only in getChat.
        /// </summary>
        [DataMember(Name = "can_set_sticker_set")]
        public bool CanSetStickerSet { get; set; }
    }
}