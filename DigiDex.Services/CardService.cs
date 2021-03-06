﻿using DigiDex.Data;
using DigiDex.Models.Card_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiDex.Services
{
    public class CardService
    {
        private readonly Guid _userId;

        public CardService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateCard(CardCreate model)
        {
            var entity =
                new Card()
                {
                    UserId = _userId,
                    CardTitle = model.CardTitle,
                    CardDescription = model.CardDescription,
                    CreatedUtc = DateTimeOffset.UtcNow,
                    DeckId = model.DeckId

                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Cards.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CardListItem> GetCards()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Cards
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new CardListItem
                                {
                                    CardId = e.CardId,
                                    CardTitle = e.CardTitle,
                                    DeckTitle = e.Deck.DeckTitle,
                                    CategoryTitle = e.Deck.Category.CategoryTitle,
                                    CreatedUtc = e.CreatedUtc,

                                }
                        );

                return query.ToArray();
            }
        }

        public CardDetail GetCardById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Cards
                        .Single(e => e.CardId == id && e.UserId == _userId);
               
                    return
                    new CardDetail
                    {
                        CardId = entity.CardId,
                        DeckTitle = entity.Deck.DeckTitle,
                        CardTitle = entity.CardTitle,
                        CardDescription = entity.CardDescription,
                        CategoryTitle = entity.Deck.Category.CategoryTitle,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc

                    };

            }
        }
        public CardDetail GetCardByTitle(string cardTitle)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Cards
                        .Single(e => e.CardTitle == cardTitle && e.UserId == _userId);
                return
                    new CardDetail
                    {
                        CardId = entity.CardId,
                        DeckTitle = entity.Deck.DeckTitle,
                        CardTitle = entity.CardTitle,
                        CardDescription = entity.CardDescription,
                        CategoryTitle = entity.Deck.Category.CategoryTitle,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };

            }
        }
        public IEnumerable<CardListItem> GetCardsByCategoryTitle(string categoryTitle)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Cards
                        .Where(e => e.Deck.Category.CategoryTitle == categoryTitle && e.UserId == _userId)
                        .Select(
                            e =>
                                new CardListItem
                                {
                                    CardId = e.CardId,
                                    CardTitle = e.CardTitle,
                                    CategoryTitle = e.Deck.Category.CategoryTitle,
                                    DeckTitle = e.Deck.DeckTitle,
                                    CreatedUtc = e.CreatedUtc
                                }
                               );
                return query.ToArray();
            }
        }
        public IEnumerable<CardListItem> GetCardsByDeckTitle(string deckTitle)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Cards
                        .Where(e => e.Deck.DeckTitle == deckTitle && e.UserId == _userId)
                        .Select(
                            e =>
                                new CardListItem
                                {
                                    CardId = e.CardId,
                                    CardTitle = e.CardTitle,
                                    DeckTitle = e.Deck.DeckTitle,
                                    CategoryTitle = e.Deck.Category.CategoryTitle,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public bool UpdateCard(CardEdit model)
        {
            using (var ctx= new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Cards
                        .Single(e => e.CardId == model.CardId && e.UserId == _userId);

                entity.CardTitle = model.CardTitle;
                entity.CardDescription = model.CardDescription;
                entity.DeckId = model.DeckId;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCard(int cardId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Cards
                        .Single(e => e.CardId == cardId && e.UserId == _userId);

                ctx.Cards.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
