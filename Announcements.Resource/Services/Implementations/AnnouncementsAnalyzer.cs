using Announcements.Resource.Domain.Entities.Implementations;
using Announcements.Resource.Domain.Repositories.Implementations;
using Announcements.Resource.Domain.Repositories.Interfaces;
using Announcements.Resource.Models;
using Announcements.Resource.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Announcements.Resource.Services.Implementations
{
    public class AnnouncementsAnalyzer : IAnnouncementsAnalyzer
    {
        private class Similarity
        {
            public Guid Id { get; set; }
            public int NumberMatches { get; set; } = 0;
        }

        protected AnnouncementsRepository Announcements { get; set; }



        public AnnouncementsAnalyzer(IAnnouncementsRepository repository)
        {
            Announcements = (AnnouncementsRepository)repository;
        }


        public IEnumerable<ShortAnnouncementModel> GetSimilar(Guid primaryId, int maxSimilarItems)
        {
            if (primaryId == Guid.Empty || maxSimilarItems == 0)    
                return null;


            AnnouncementEntity primaryAnn = Announcements.Get(primaryId);

            List<Similarity> similar = Announcements
                .Select(ann => new Similarity()
                {
                    Id = ann.Id,
                    NumberMatches = CountSameWords(primaryAnn, ann)
                })
                .ToList();

            return similar
                .OrderByDescending(s => s.NumberMatches)
                .Skip(1)    // Skip first, because it is primary Announcement
                .Take(maxSimilarItems)
                .Select(sm => new ShortAnnouncementModel()
                {
                    Id = sm.Id,
                    Title = Announcements.Get(sm.Id).Title
                });
        }

        public static int CountSameWords(AnnouncementEntity primary, AnnouncementEntity comparable)
        {
            char[] delimiters = { ' ', ',', '.', '!', '?', ':', '\n' };

            List<string> primaryWords = $"{primary.Title} {primary.Description}"
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            List<string> compWords = $"{comparable.Title} {comparable.Description}"
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            int matches = primaryWords
                .Join(compWords,
                      pWord => pWord,
                      cWord => cWord,
                      (pWord, cWord) => pWord == cWord)
                .Count();

            return matches;
        }
    }

}
