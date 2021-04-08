

select * from AspNetUsers
select * from Recipe
select * from Ingredient
select * from Comment
select * from Heart

insert into Heart(ApplicationUserId, RecipeId, CreatedOn)
values ('8802264b-64ac-4991-838c-cf4f303fbcf8', 4, GETDATE())

insert into Recipe(Title, CreatedOn, ApplicationUserId, Description, Instructions, Category, CookTime, MealType, PrepTime, Servings, SpiceLevel)
values ('asdasda', GETDATE(), '8802264b-64ac-4991-838c-cf4f303fbcf8', 'aslkdj;ajd alsdkja;skdj asdlkja;sdlkj asd;lkja', 'alskdjlaksjdlaskjdlaskdjlaskjd asldkjlaskjdlaskjdlsa asdlkjalsdkjaldkjald', 1, 10, 2, 15, 2, 0)

delete Comment
where Id > 120

delete Ingredient
where RecipeId = 4

select * from Recipe
select * from AspNetUsers
select * from Heart
select * from Ingredient where RecipeId = 4

delete Heart
where RecipeId = 15

insert into Heart(ApplicationUserId, RecipeId, CreatedOn)
values('63a951b7-acb3-4a77-a062-f0e3a8dadd32', 15, GETDATE())
