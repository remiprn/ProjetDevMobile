using ProjetDevMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetDevMobile.Services
{
    public interface ITagService
    {
        List<Tag> GetTags();

        Tag GetTag(int pos);

        void AddTag(Tag tag);

        void DeleteTag(Tag tag);
    }
}
