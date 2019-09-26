// Иногда на страницах регистрации требуют выбрать, например, несколько случайных тем для чтения.
// Данный сниппет поможет выбрать и кликнуть по N случайных элементов без дублирования клика.
// То есть не будет ситуации, что будет 2 раза клик по одному и тому же элементу
// (что в итоге может привести к тому, что если надо выбрать 5 элементов,
//  а были дубли в кликах, то выбралось 3 и кнопка "далее" не активна)

Random rnd = new Random();

// XPath для поиска всех элементов, по которым нужно кликнуть
HtmlElementCollection hec = instance.ActiveTab.FindElementsByXPath("//div[contains(@class,'category')]/a[@class='category-item']");
if (hec.Count == 0) {
    throw new Exception("hec empty");
}

// конвертируем коллекцию элементов, чтоб не делать черный список и не перепроверять по нему
List<HtmlElement> hec_lst = hec.Elements.ToList();

// сколько нужно выбрать (от 5 до 9, верхняя граница не включительно)
int count = rnd.Next(5, 9);

// на всякий случай берем минимальное от необходимого количества для клика и общего количества
count = Math.Min(count, hec_lst.Count);

for(int i = 0; i < count; i++) {
    // берем случайны элемент
    int he_indx = rnd.Next(hec_lst.Count);
    HtmlElement he = hec_lst[he_indx];

    // кликаем
    he.Click();

    // удаляем элемент из списка
    hec_lst.RemoveAt(he_indx);

    // пауза
    new System.Threading.ManualResetEvent(false).WaitOne(1000);
}