using System;
using System.Collections.Generic;
using System.Text;
using ProjetDevMobile.Models;

namespace ProjetDevMobile.Services
{
    class TagService : ITagService
    {
        private List<Tag> _tags;

        public TagService()
        {
            _tags = new List<Tag>();

            Tag tag = new Tag("#Drink");
            _tags.Add(tag);

            tag = new Tag("#Food");
            _tags.Add(tag);

            tag = new Tag("#ToSee");
            _tags.Add(tag);
        }

        public void AddTag(Tag tag)
        {
            _tags.Add(tag);
        }

        public void DeleteTag(Tag tag)
        {
            _tags.Remove(tag);
        }

        public Tag GetTag(int pos)
        {
            return _tags[pos];
        }

        public List<Tag> GetTags()
        {
            return _tags;
        }
    }
}
