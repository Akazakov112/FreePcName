// Создать объект для Http запросов.
let request = new XMLHttpRequest();
// Установка ожидаемого типа для ответа на запрос.
request.responseType = 'json';

// Получить все элементы тега option.
let osps = document.querySelectorAll('option');

// Каждому элементу option названить обработчик двойного нажатия.
for (let i = 0; i < osps.length; i++) {
    osps[i].addEventListener('dblclick', getFreeNames);
}

// Обработчик для события нажатия.
function getFreeNames() {

    // Объект FormData, берем данные формы для отправки.
    let formData = new FormData(document.forms.getNameForm);

    // Конфигурация запроса.
    request.open('POST', 'freepcname/main/index', true);

    // Если установлено поле в форме отправки.
    if (formData.has('selectOsp')) {
        // Отправляем запрос с данными формы.
        request.send(formData);
    }
    // Иначе вывести сообщение о необходимости выбора ОСП.
    else {
        alert('Выберите ОСП.');
    }
}

// Добавить обработчик для загрузки ответа на запрос.
request.addEventListener('load', function () {

    // Получить ответ.
    let jsonResponse = request.response;

    // Получить элементы для вывода информации по Id.
    let pcNameLbl = document.getElementById('pcName');
    let noteNameLbl = document.getElementById('noteName');
    let armNameLbl = document.getElementById('armName');

    // Вывести информацию из ответа на страницу.
    pcNameLbl.innerHTML = `Первое свободное имя пк: <strong>${jsonResponse.pcName}</strong>`;
    noteNameLbl.innerHTML = `Первое свободное имя ноутбука: <strong>${jsonResponse.notebookName}</strong>`;
    armNameLbl.innerHTML = `Первое свободное имя АРМ: <strong>${jsonResponse.armName}</strong>`;

});