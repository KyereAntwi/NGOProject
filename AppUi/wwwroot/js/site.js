const button = document.getElementById('fileUpload');
const inputControl = document.getElementById('fileInput');

button.addEventListener('click', (e) => {
    e.preventDefault();
    inputControl.click();
});

inputControl.addEventListener('change', (e) => {
    var imgHandle = document.getElementById('fileImage');

    var reader = new FileReader();
    reader.onload = (e) => {
        imgHandle.src = e.target.result;
    }

    reader.readAsDataURL(e.srcElement.files[0]);
})