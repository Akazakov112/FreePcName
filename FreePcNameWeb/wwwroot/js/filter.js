// Получить элемент страницы по Id.
let searchString = document.getElementById('searchString');

// Привязка обработчика события при нажатии клавиши.
searchString.addEventListener('keyup', function filterOsps() {

    // Получить элемент документа с Id = selectOsp.
    let selectOsp = document.getElementById('selectOsp');
    // Получить все дочерние элементы с тэгом option в элементе selectOsp.
    let options = selectOsp.getElementsByTagName('option');
    // Получить значение в элементе поисковой строки.
    let filter = searchString.value.toUpperCase();

    // Перебираем коллекцию элементов option.
    for (let i = 0; i < options.length; i++) {

        // Если в тексте или значении элемента есть вхождение поисковой строки.
        if (options[i].text.toUpperCase().indexOf(filter) > -1 || options[i].value.toUpperCase().indexOf(filter) > -1) {
            // То установить стиль элемента на отображение.
            options[i].style.display = "";
        }
        // Иначе установить стиль элемента на скрытие.
        else {
            options[i].style.display = "none";
        }
    }
});