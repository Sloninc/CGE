# CGE
1) Напишите консольное приложение на C#, которое на вход
принимает большой текстовый файл (например, «Война и мир», можно взять
отсюда: http://az.lib.ru/). На выходе создает текстовый файл с перечислением
всех уникальных слов, встречающихся в тесте и количеством их
употреблений, отсортированный в порядке убывания количества
употреблений, например,
d'artifice 50
говорит 48
значительно 30
Задачу выполните двумя методами: однопоточно и параллельно.
Добавьте таймеры на оба метода. Выполните их за один запуск поочерёдно, а
время работы каждого выведите в консоль. Приложите к ответу текстовый
файл, который использовался в программе.

2) Создайте экземпляр абстрактного класса с помощью рефлексии.
* По возможности: все переменные класса, которые имеют значения,
должны быть инициализированы этими значениями при создании экземпляра
абстрактного класса. К примеру, существует класс Cat:
public abstract class Cat
{
public int age = 4;
public string name;
public int tailCount = 1;
}
В методе Main класса Program необходимо создать экземпляр этого класса
так, чтобы переменные были инициализированы (их количество может быть
разное, не обязательно как в примере, но все они должны быть
инициализированы).

3) Дана БД, имеющая две таблицы: сотрудники и подразделение.
Необходимо написать 5 запросов.
Напишите запросы, которые выведут:
1. Сотрудника с максимальной заработной платой.
2. Отдел с самой высокой заработной платой между сотрудниками.
3. Отдел с максимальной суммарной зарплатой сотрудников.
4. Сотрудника, чье имя начинается на «Р» и заканчивается на «н».
   
4) Маша пришла в спортивный зал и хочет позаниматься на тренажёре
с нагрузкой в 16 килограммов. Рядом лежит груда чугунных блинов.
Девушка видит, что пудовых грузов в наборе сейчас нет, значит, одним
блином не обойтись, а больше двух на тренажёр не установить.
Маше нужно найти два блина (или один, если такой будет иметься в
наличии), которые в сумме дают 16 килограммов, и при этом не потратить на
поиск всё отведённое на тренировку время. Гарантируется, что это
выполнимая задача и такие два блина точно есть: об этом Маше рассказал её
друг, который занимался на этом же тренажёре с той же нагрузкой.
Составьте на C# алгоритм, по которому Маша сможет получить
необходимый для тренажёра вес. Так как девушка купила премиум
абонемент, подразумевается, что в зале могут встречаться блины
всевозможных размеров.
