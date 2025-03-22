<script lang="ts">
  import { onMount } from 'svelte';
  import { scrollFadeIn } from '../../animations';

  let content: HTMLElement;

  onMount(() => {
    scrollFadeIn(content, { delay: 0.3 });
  });
</script>

<style>
  .container {
    padding: 2rem;
    max-width: 800px;
    margin: 0 auto;
    text-align: center;
    background-color: rgba(0, 0, 0, 0.8);
    color: #fff;
    border-radius: 10px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.5);
  }

  h1 {
    font-family: 'MedievalSharp', serif;
    color: #f9d342;
    margin-bottom: 2rem;
  }

  .buttons {
    display: flex;
    flex-direction: column;
    gap: 1rem;
  }

  .button {
    background: linear-gradient(90deg, #5b1e14, #8a261c);
    color: #fff;
    padding: 0.8rem;
    text-transform: uppercase;
    text-decoration: none;
    font-size: 1.2rem;
    border-radius: 5px;
    transition: transform 0.3s ease, background 0.3s ease;
  }

  .button:hover {
    transform: translateY(-5px);
    background: linear-gradient(90deg, #8a261c, #5b1e14);
  }
</style>

<div bind:this={content} class="container">
  <h1>Télécharger FairyWood</h1>
  <p>Choisissez votre plateforme pour télécharger FairyWood.</p>
  <div class="buttons">
    <a href="/static/FairyWood-windows.zip" class="button">Télécharger pour Windows</a>
    <a href="/static/FairyWood-mac.zip" class="button">Télécharger pour Mac</a>
    <a href="/static/FairyWood-linux.zip" class="button">Télécharger pour Linux</a>
  </div>
</div>