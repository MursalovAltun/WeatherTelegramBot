using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.DTO
{
    /// <summary>
    /// This object represents a message.
    /// </summary>
    public class MessageDTO
    {
        /// <summary>
        /// Unique message identifier inside this chat
        /// </summary>
        [DataMember(Name = "message_id")]
        public int Id { get; set; }

        /// <summary>
        /// Optional
        /// Sender, empty for messages sent to channels
        /// </summary>
        [DataMember(Name = "from")]
        public UserDTO From { get; set; }

        /// <summary>
        /// Date the message was sent in Unix time
        /// </summary>
        [DataMember(Name = "date")]
        public long Date { get; set; }

        /// <summary>
        /// Conversation the message belongs to
        /// </summary>
        [DataMember(Name = "chat")]
        public ChatDTO Chat { get; set; }

        /// <summary>
        /// Optional
        /// For forwarded messages, sender of the original message
        /// </summary>
        [DataMember(Name = "forward_from")]
        public UserDTO ForwardFrom { get; set; }

        /// <summary>
        /// Optional
        /// For messages forwarded from channels, information about the original channel
        /// </summary>
        [DataMember(Name = "forward_from_chat")]
        public ChatDTO ForwardFromChat { get; set; }

        /// <summary>
        /// Optional
        /// For messages forwarded from channels, identifier of the original message in the channel
        /// </summary>
        [DataMember(Name = "forward_from_message_id")]
        public int ForwardFromMessageId { get; set; }

        /// <summary>
        /// Optional
        /// For messages forwarded from channels, signature of the post author if present
        /// </summary>
        [DataMember(Name = "forward_signature")]
        public string ForwardSignature { get; set; }

        /// <summary>
        /// Optional
        /// Sender's name for messages forwarded from users who disallow
        /// adding a link to their account in forwarded messages
        /// </summary>
        [DataMember(Name = "forward_sender_name")]
        public string ForwardSenderName { get; set; }

        /// <summary>
        /// Optional
        /// For forwarded messages, date the original message was sent in Unix time
        /// </summary>
        [DataMember(Name = "forward_date")]
        public long ForwardDate { get; set; }

        /// <summary>
        /// Optional
        /// For replies, the original message.
        /// Note that the Message object in this field will
        /// not contain further reply_to_message fields even if it itself is a reply.
        /// </summary>
        [DataMember(Name = "reply_to_message")]
        public MessageDTO ReplyToMessage { get; set; }

        /// <summary>
        /// Optional
        /// Date the message was last edited in Unix time
        /// </summary>
        [DataMember(Name = "edit_date")]
        public long EditDate { get; set; }

        /// <summary>
        /// Optional
        /// The unique identifier of a media message group this message belongs to
        /// </summary>
        [DataMember(Name = "media_group_id")]
        public string MediaGroupId { get; set; }

        /// <summary>
        /// Optional
        /// Signature of the post author for messages in channels
        /// </summary>
        [DataMember(Name = "author_signature")]
        public string AuthorSignature { get; set; }

        /// <summary>
        /// Optional
        /// For text messages, the actual UTF-8 text of the message, 0-4096 characters
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Optional
        /// For text messages, special entities like usernames, URLs,
        /// bot commands, etc. that appear in the text
        /// </summary>
        [DataMember(Name = "entities")]
        public IEnumerable<MessageEntityDTO> Entities { get; set; }

        /// <summary>
        /// Optional
        /// For messages with a caption, special entities like usernames,
        /// URLs, bot commands, etc. that appear in the caption
        /// </summary>
        [DataMember(Name = "caption_entities")]
        public IEnumerable<MessageEntityDTO> CaptionEntites { get; set; }

        /// <summary>
        /// Optional
        /// Message is an audio file, information about the file
        /// </summary>
        [DataMember(Name = "audio")]
        public AudioDTO Audio { get; set; }

        /// <summary>
        /// Optional
        /// Message is a general file, information about the file
        /// </summary>
        [DataMember(Name = "document")]
        public DocumentDTO Document { get; set; }

        /// <summary>
        /// Optional
        /// Message is an animation, information about the animation.
        /// For backward compatibility, when this field is set, the document field will also be set
        /// </summary>
        [DataMember(Name = "animation")]
        public AnimationDTO Animation { get; set; }
    }
}