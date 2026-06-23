function ObtenerNotas() {
    fetch('https://localhost:7187/NotaAlumnos')
        .then(r => r.json())
        .then(data => MostrarNotas(data))
        .catch(e => console.error(e));
}

function MostrarNotas(data) {
    $("#TablaNotas").empty();

    data.forEach(n => {
        console.log(n);

        $("#TablaNotas").append(`
            <tr>
                <td>${n.notaAlumnoId}</td>
                <td>${n.nombreCompleto}</td>
                <td>${n.asignaturaNombre}</td>
                <td>${n.nota}</td>
                <td>
                    <button class="btn btn-info"
                        onclick="BuscarNota(${n.notaAlumnoId})">
                        Editar
                    </button>
                </td>
                <td>
                    <button class="btn btn-warning"
                    onclick="AbrirModalHistorial(${n.notaAlumnoId})">
                    Historial
                </button>
                </td>
                <td>
                    <button class="btn btn-danger"
                        onclick="EliminarNota(${n.notaAlumnoId})">
                        Eliminar
                    </button>
                </td>
            </tr>
        `);
    });
}

function CargarAlumnos() {

    fetch('https://localhost:7187/Api/Alumnos')
        .then(r => r.json())
        .then(data => {
            let select = $("#AlumnosId, #AlumnoIdEditar");
            select.empty();

            data.forEach(a => {
                select.append(`<option value="${a.alumnoId}">${a.nombreCompleto}</option>`);
            });
        });
}

function CargarAsignaturas() {
    fetch('https://localhost:7187/Asignaturas')
        .then(r => r.json())
        .then(data => {
            let select = $("#AsignaturasId, #AsignaturaIdEditar");
            select.empty();

            data.forEach(a => {
                select.append(`<option value="${a.asignaturaId}">${a.descripcion}</option>`);
            });
        });
}

function CrearNota() {

    let nota = document.getElementById("Nota").value;

    if (nota < 1 || nota > 10) {
        alert("Nota inválida");
        return;
    }

    const hoy = new Date();
    const fechaFormateada = new Date().toISOString();
    let obj = {
        alumnoId: parseInt(document.getElementById("AlumnosId").value),
        asignaturaId: parseInt(document.getElementById("AsignaturasId").value),
        nota: parseInt(nota),
        fechaNota: fechaFormateada
    };

    fetch('https://localhost:7187/NotaAlumnos', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(obj)
    })
        .then(r => r.json())
        .then(() => {
            let modal = bootstrap.Modal.getInstance(document.getElementById('ModalAgregarNotaAlumno'));

            if (!modal) {
                modal = new bootstrap.Modal(document.getElementById('ModalAgregarNotaAlumno'));
            }

            modal.hide();
            ObtenerNotas();
        });
}

function BuscarNota(id) {
    fetch(`https://localhost:7187/NotaAlumnos/${id}`)
        .then(r => r.json())
        .then(n => {

            document.getElementById("IdEditar").value = n.notaAlumnoId;
            document.getElementById("AlumnoIdEditar").value = n.alumnoId;
            document.getElementById("AsignaturaIdEditar").value = n.asignaturaId;
            document.getElementById("NotaEditar").value = n.nota;

            new bootstrap.Modal(document.getElementById("ModalEditarNotaAlumno")).show();
        });
}

function EditarNota() {

    let nota = document.getElementById("NotaEditar").value;

    if (nota < 1 || nota > 10) {
        alert("Nota inválida");
        return;
    }

    let obj = {
        notaAlumnoId: parseInt(document.getElementById("IdEditar").value),
        alumnoId: parseInt(document.getElementById("AlumnoIdEditar").value),
        asignaturaId: parseInt(document.getElementById("AsignaturaIdEditar").value),
        nota: parseInt(document.getElementById("NotaEditar").value)
    };

    fetch(`https://localhost:7187/NotaAlumnos/${obj.notaAlumnoId}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(obj)
    })
        .then(() => {
            let modal = bootstrap.Modal.getInstance(document.getElementById('ModalEditarNotaAlumno'));

            if (!modal) {
                modal = new bootstrap.Modal(document.getElementById('ModalEditarNotaAlumno'));
            }

            modal.hide();
            ObtenerNotas();
        });
}

function EliminarNota(id) {
    if (!confirm("¿Eliminar nota?")) return;

    fetch(`https://localhost:7187/NotaAlumnos/${id}`, {
        method: 'DELETE'
    })
        .then(() => ObtenerNotas());
}

async function AbrirModalHistorial(id) {

    try {
        const respuesta = await fetch(`https://localhost:7187/Api/Informes/HistorialNotas/${id}`,
            {
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                }
            }
        );

        if (!respuesta.ok) {
            throw new Error("No se pudo obtener el dato");
        }

        const historial = await respuesta.json();

        const bodyNotasAlumnos = document.getElementById("tbody-historial-notas");
        bodyNotasAlumnos.innerHTML = "";

        historial.forEach((nota) => {
            const tr = document.createElement("tr");

            tr.innerHTML = `
       <td class="text-center">${nota.fechaCambioString} Hs.</td>
            <td>${nota.campoModificado}</td>
            <td>${nota.valorAnterior} </td>
              <td>${nota.valorNuevo} </td>
        `;

            bodyNotasAlumnos.appendChild(tr);
        });


        var modal = bootstrap.Modal.getOrCreateInstance(
            document.getElementById('modalHistorialNotaAlumno')
        );

        modal.show();

    } catch (error) {
        console.error("Error editar:", error);
    }
}