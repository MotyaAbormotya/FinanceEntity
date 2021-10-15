using Pattern.Model.FinanceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern.Model
{
    public class FinanceData
    {
        private List<Income> _incomes;
        private List<Outcome> _outcomes;
        private List<IncomeCategory> _incomeCategories;
        private List<OutcomeCategory> _outcomeCategories;

        public FinanceData()
        {
            _incomes = new List<Income>();
            _outcomes = new List<Outcome>();
            _incomeCategories = new List<IncomeCategory>();
            _outcomeCategories = new List<OutcomeCategory>();
        }

        #region Income CRUD
        public bool AddIncome(int sum, int categoryId, DateTime dateTime)
        {
            Category category = (Category)GetIncomeCategoryById(categoryId);

            if (category != null)
                _incomes.Add(new Income(sum, category, dateTime));
            else
                return false;

            return true;
        }

        public bool RemoveIncome(int id) => RemoveFinanceEntity(id, _incomes);

        public bool UpdateIncome(int incomeID, int sum = -1, int categoryId = -1) =>
            UpdateFinanceEntity(incomeID, _incomes, GetIncomeCategoryById, sum, categoryId);

        public IReadOnlyFinanceEntity GetIncomeById(int id) => GetFinanceEntityById(id, _incomes);

        public IReadOnlyList<IReadOnlyFinanceEntity> GetIncomes() => _incomes;
        #endregion

        #region Outcome CRUD
        public bool AddOutcome(int sum, int categoryId, DateTime dateTime)
        {
            Category category = (Category)GetIncomeCategoryById(categoryId);

            if (category != null)
                _outcomes.Add(new Outcome(sum, category, dateTime));
            else
                return false;

            return true;
        }

        public bool RemoveOutcome(int id) => RemoveFinanceEntity(id, _outcomes);

        public bool UpdateOutcome(int outcomeID, int sum = -1, int categoryId = -1) =>
            UpdateFinanceEntity(outcomeID, _outcomes, GetOutcomeCategoryById, sum, categoryId);

        public IReadOnlyFinanceEntity GetOutcomeById(int id) => GetFinanceEntityById(id, _outcomes);

        public IReadOnlyList<Outcome> GetOutcomes() => _outcomes;
        #endregion

        #region IncomeCategories CRUD
        public IReadOnlyCategory GetIncomeCategoryById(int categoryId) => GetCategoryById(categoryId, _incomeCategories);

        public IReadOnlyList<IReadOnlyCategory> GetIncomeCategories() => _incomeCategories;

        public bool AddIncomeCategory(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return false;

            foreach (var item in _incomeCategories)
                if (title == item.Title)
                    return false;

            _incomeCategories.Add(new IncomeCategory(title));
            return true;
        }

        public bool RemoveIncomeCategory(int id)
        {
            int index = FindIncomeCategoryById(id);
            if (index != -1)
            {
                _incomeCategories.RemoveAt(index);
                return true;
            }

            return false;
        }

        public bool UpdateIncomeCategory(int id, string newTitle)
        {
            int index = FindIncomeCategoryById(id);
            if (index != -1 && string.IsNullOrWhiteSpace(newTitle))
            {
                _incomeCategories[index].Update(newTitle);
                return true;
            }

            return false;
        }

        private int FindIncomeCategoryById(int id)
        {
            for (int i = 0; i < _incomeCategories.Count; i++)
                if (id == _incomeCategories[i].ID)
                    return i;

            return -1;
        }
        #endregion

        #region OutcomeCategories CRUD
        public Category GetOutcomeCategoryById(int categoryId) => GetCategoryById(categoryId, _outcomeCategories);

        public bool AddOutcomeCategory(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return false;

            foreach (var item in _outcomeCategories)
                if (title == item.Title)
                    return false;

            _outcomeCategories.Add(new OutcomeCategory(title));
            return true;
        }

        public bool RemoveOutcomeCategory(int id)
        {
            int index = FindOutcomeCategoryById(id);
            if (index != -1)
            {
                _outcomeCategories.RemoveAt(index);
                return true;
            }

            return false;
        }

        public bool UpdateOutcomeCategory(int id, string newTitle)
        {
            int index = FindOutcomeCategoryById(id);
            if (index != -1 && string.IsNullOrWhiteSpace(newTitle))
            {
                _outcomeCategories[index].Update(newTitle);
                return true;
            }

            return false;
        }

        private int FindOutcomeCategoryById(int id)
        {
            for (int i = 0; i < _outcomeCategories.Count; i++)
                if (id == _outcomeCategories[i].ID)
                    return i;

            return -1;
        }
        #endregion

        #region Private Finance Data Methods
        private Category GetCategoryById(int categoryId, IEnumerable<Category> categories)
        {
            foreach (var category in categories)
                if (category.ID == categoryId)
                    return category;

            return null;
        }

        private bool RemoveFinanceEntity(int id, IEnumerable<FinanceEntity> entities)
        {
            int index = FindIndexFinanceEntityById(id, (List<FinanceEntity>)entities);

            if (index != -1)
            {
                ((List<FinanceEntity>)entities).RemoveAt(index);
                return true;
            }

            return false;
        }

        private bool UpdateFinanceEntity(int ID, IEnumerable<FinanceEntity> entities, Func<int, IReadOnlyCategory> getCategory, int sum = -1, int categoryId = -1)
        {
            int index = FindIndexFinanceEntityById(ID, (List<FinanceEntity>)entities);

            if (index != -1)
            {
                Category category = categoryId == -1 ? null : (Category)getCategory(categoryId);
                ((List<FinanceEntity>)entities)[index].Update(sum, category);
                return true;
            }

            return false;
        }

        private int FindIndexFinanceEntityById(int Id, IEnumerable<FinanceEntity> entities)
        {
            List<FinanceEntity> entitiesList = (List<FinanceEntity>)entities;

            for (int i = 0; i < entitiesList.Count; i++)
                if (entitiesList[i].ID == Id)
                    return i;

            return -1;
        }

        private IReadOnlyFinanceEntity GetFinanceEntityById(int id, IEnumerable<FinanceEntity> entities)
        {
            int index = FindIndexFinanceEntityById(id, entities);

            if (index != -1)
                return ((List<FinanceEntity>)entities)[index];
            else
                return null;
        }

        private bool RemoveCategory(int id, IEnumerable<Category> entities)
        {
            int index = FindIncomeCategoryById(id);

            if (index != -1)
            {
                ((List<Category>)entities).RemoveAt(index);
                return true;
            }

            return false;
        }

        private bool UpdateCategory(int id, string newTitle, IEnumerable<Category> entities)
        {
            int index = FindCategoryById(id, ((List<Category>)entities));

            if (index != -1 && string.IsNullOrWhiteSpace(newTitle))
            {
                ((List<Category>)entities)[index].Update(newTitle);
                return true;
            }

            return false;
        }

        private int FindCategoryById(int id, IEnumerable<Category> entities)
        {
            List<Category> entitiesList = (List<Category>)entities;

            for (int i = 0; i < entitiesList.Count; i++)
                if (id == entitiesList[i].ID)
                    return i;

            return -1;
        }
        #endregion
    }
}
