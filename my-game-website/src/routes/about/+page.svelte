<script lang="ts">
  import { onMount } from 'svelte';
  import { scrollSlideIn } from '../../animations';

  let teamSection: HTMLElement;

  onMount(() => {
    scrollSlideIn(teamSection, { delay: 0.4, x: -100 });
  });
</script>

<style>
  .container {
    padding: 2rem;
    max-width: 800px;
    margin: 0 auto;
    background-color: rgba(0, 0, 0, 0.8);
    color: #fff;
    border-radius: 10px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.5);
  }

  h1 {
    font-family: 'MedievalSharp', serif;
    color: #f9d342;
    margin-bottom: 2rem;
    text-align: center;
  }

  p {
    font-size: 1.2rem;
    line-height: 1.8;
    text-align: justify;
  }

  .button {
    display: inline-block;
    background-color: #0056b3;
    color: #fff;
    padding: 0.5rem 1rem;
    text-transform: uppercase;
    text-decoration: none;
    font-size: 1rem;
    border-radius: 5px;
    margin-top: 1rem;
    transition: transform 0.3s ease, background-color 0.3s ease;
  }

  .button:hover {
    transform: translateY(-3px);
    background-color: #003a80;
  }
</style>

<div bind:this={teamSection} class="container">
  <h1>À propos de nous</h1>
  <p>
    FairyWood est un RPG 2D développé par une équipe passionnée. Nous avons à cœur de créer
    une expérience immersive pour les amateurs de mondes fantastiques.
  </p>
  <a href="/static/team-profile.pdf" class="button">PDF de l'équipe</a>
</div>