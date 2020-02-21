using System.Runtime.Serialization;

namespace Common.DTO
{
    /// <summary>
    /// Describes actions that a non-administrator user is allowed to take in a chat.
    /// </summary>
    public class CharPermisstionsDTO
    {
        /// <summary>
        /// Optional
        /// True, if the user is allowed to send text messages, contacts, locations and venues
        /// </summary>
        [DataMember(Name = "can_send_messages")]
        public bool CanSendMessages { get; set; }

        /// <summary>
        /// Optional
        /// True, if the user is allowed to send audios, documents, photos,
        /// videos, video notes and voice notes, implies can_send_messages
        /// </summary>
        [DataMember(Name = "can_send_media_messages")]
        public bool CanSendMediaMessages { get; set; }

        /// <summary>
        /// Optional
        /// True, if the user is allowed to send polls, implies can_send_messages
        /// </summary>
        [DataMember(Name = "can_send_polls")]
        public bool CanSendPolls { get; set; }

        /// <summary>
        /// Optional
        /// True, if the user is allowed to send animations, games,
        /// stickers and use inline bots, implies can_send_media_messages
        /// </summary>
        [DataMember(Name = "can_send_other_messages")]
        public bool CanSendOtherMessages { get; set; }

        /// <summary>
        /// Optional
        /// True, if the user is allowed to add web page previews to their messages,
        /// implies can_send_media_messages
        /// </summary>
        [DataMember(Name = "can_add_web_page_previews")]
        public bool CanAndWebPagePreviews { get; set; }

        /// <summary>
        /// Optional
        /// True, if the user is allowed to change the chat title, photo and other settings.
        /// Ignored in public supergroups
        /// </summary>
        [DataMember(Name = "can_change_info")]
        public bool CanChangeInfo { get; set; }

        /// <summary>
        /// Optional
        /// True, if the user is allowed to invite new users to the chat
        /// </summary>
        [DataMember(Name = "can_invite_users")]
        public bool CanInviteUsers { get; set; }

        /// <summary>
        /// Optional
        /// True, if the user is allowed to pin messages.
        /// Ignored in public supergroups
        /// </summary>
        [DataMember(Name = "can_pin_messages")]
        public bool CanPinMessages { get; set; }
    }
}