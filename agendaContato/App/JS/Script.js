function incluirHTML() {
  const elementos = document.querySelectorAll('[data-include]');

  elementos.forEach(async (el) => {
    const arquivo = el.getAttribute('data-include');
    const resp = await fetch(arquivo);
    if (resp.ok) {
      const conteudo = await resp.text();
      el.innerHTML = conteudo;
    } else {
      el.innerHTML = "Erro ao carregar " + arquivo;
    }
  });
}

document.addEventListener("DOMContentLoaded", incluirHTML);