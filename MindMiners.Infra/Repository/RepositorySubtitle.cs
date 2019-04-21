using MindMiners.Domain.Entities;
using MindMiners.Domain.Interface;
using MindMiners.Infra.Repository.Generic;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindMiners.Infra.Repository
{
    public class RepositorySubtitle : RepositoryGeneric<SubtitleEntity>, InterfaceSubtitle
    {

        public RepositorySubtitle(): base("subtitles")
        {
        }

        public new void Dispose()
        {
        }

        public void Add(SubtitleEntity entity)
        {
            
            var cloneEntity = (SubtitleEntity)entity.Clone();
            cloneEntity.ToSrtFile();
            cloneEntity.Lines = null;
            base.Add(cloneEntity);
        }

        public SubtitleEntity GetByName(string name)
        {
        
            var item = this.collection.Find(x => x.Name == name)
                    .Project<SubtitleEntity>("{_id: 0}").FirstOrDefault();


            return item;
        }






    }
}
