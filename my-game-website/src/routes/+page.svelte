<script lang="ts">
  import { onMount } from 'svelte';
  import { scrollFadeIn, scrollSlideIn } from '../animations';


  
  let sections = [
    { ref: null, animation: scrollFadeIn, options: { delay: 0.2 } },
    { ref: null, animation: scrollSlideIn, options: { delay: 0.4, x: -100 } },
    { ref: null, animation: scrollFadeIn, options: { delay: 0.6 } },
    { ref: null, animation: scrollSlideIn, options: { delay: 0.8, x: 100 } },
  ];

  onMount(() => {
    sections.forEach(({ ref, animation, options }) => {
      if (ref) animation(ref, options);
    });
  });
</script>

<style>
  body {
    font-family: 'MedievalSharp', serif;
    color: #fff;
    background-color: #111;
  }

  h1, h2, h3 {
    text-align: center;
    color: #f9d342;
    margin-bottom: 1.5rem;
  }

  p {
    font-size: 1.2rem;
    line-height: 1.8;
    margin: 0 auto;
    max-width: 800px;
  }

  .section {
    margin: 4rem 0;
    padding: 0 2rem;
  }

  .features-section {
    text-align: center;
    padding: 4rem 2rem;
  }

  .features-grid {
    display: flex;
    justify-content: center;
    gap: 9.5rem;
  }

  .feature-item {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    text-align: left;
    width: 300px;
  }

  .custom-box {
    background-size: cover;
    background-repeat: no-repeat;
    background-position: center;
    width: 300px;
    height: 200px;
    border-radius: 0;
    margin: 0 auto;
  }

  .feature-title {
    margin-top: 1rem;
    font-size: 1.5rem;
    color: #fff;
  }

  .feature-description {
    font-size: 1rem;
    color: #fff;
    margin: -1rem 0 1rem;
    font-weight: 400;
  }

  .learn-more {
    color: #f9d342;
    font-size: 1rem;
    text-transform: uppercase;
    text-decoration: none;
    border-bottom: 1px solid #f9d342;
    padding-bottom: 0.2rem;
    transition: color 0.3s ease, border-color 0.3s ease;
  }

  .learn-more:hover {
    color: #fff;
    border-color: #fff;
  }

  .buttons {
    display: flex;
    justify-content: center;
    gap: 2rem;
    margin: 2rem 0;
  }

  .button {
    font-family: 'MedievalSharp', serif;
    background: linear-gradient(90deg, #5b1e14, #8a261c);
    color: #fff;
    font-size: 1.1rem;
    padding: 0.6rem 1.5rem;
    border: 2px solid #f9d342;
    border-radius: 5px;
    text-transform: uppercase;
    text-decoration: none;
    transition: transform 0.3s ease, background 0.3s ease;
  }

  .button:hover {
    transform: scale(1.05);
    background: linear-gradient(90deg, #8a261c, #5b1e14);
  }

  .roadmap {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
    gap: 2rem;
    margin-top: 2rem;
  }

  .roadmap-item {
    border: 1px solid rgba(255, 255, 255, 0.2);
    border-radius: 10px;
    padding: 1.5rem;
    background-color: rgba(255, 255, 255, 0.05);
    text-align: center;
    transition: transform 0.3s ease, background-color 0.3s ease;
  }

  .roadmap-item:hover {
    transform: scale(1.05);
    background-color: rgba(255, 255, 255, 0.1);
  }

  .roadmap-item h3 {
    color: #f9d342;
    margin-bottom: 1rem;
  }

  .roadmap-item p {
    font-size: 1.2rem;
  }

  .intro-text {
    font-size: 1.2rem;
    line-height: 1.8;
    text-align: center;
    margin: 0 auto;
    max-width: 800px;
  }
</style>

<div>
  <!-- Introduction Section -->
  <section bind:this={sections[0].ref} class="section">
    <h1>Bienvenue à FairyWood</h1>
    <p class="intro-text">
      Plongez dans un monde magique où l'aventure, la stratégie, et l'exploration s'entrelacent. 
      FairyWood est un RPG 2D conçu pour ravir les amateurs de mondes fantastiques.
    </p>
  </section>

  <!-- Buttons Section -->
  <div class="buttons">
    <a href="/download" class="button">Télécharger</a>
    <a href="/about" class="button">À propos</a>
  </div>

  <!-- Features Section -->
  <section bind:this={sections[2].ref} class="features-section">
    <h2>Caractéristiques du Jeu</h2>
    <div class="features-grid">
      <div class="feature-item">
        <div class="custom-box" style="background-image: url('/box1.png');"></div>
        <h3 class="feature-title">Items</h3>
        <p class="feature-description">
          Découvrez des objets puissants pour vous équiper, attaquer ou défendre contre des ennemis redoutables.
        </p>
        <a href="/items" class="learn-more">En savoir plus</a>
      </div>
      <div class="feature-item">
        <div class="custom-box" style="background-image: url('/box2.png');"></div>
        <h3 class="feature-title">Roles</h3>
        <p class="feature-description">
          Travaillez en équipe pour vaincre le boss final.
        </p>
        <a href="/roles" class="learn-more">En savoir plus</a>
      </div>
      <div class="feature-item">
        <div class="custom-box" style="background-image: url('/box1.png');"></div>
        <h3 class="feature-title">Map</h3>
        <p class="feature-description">
          Explorez des environnements détaillés, des lieux mystérieux et des zones remplies de secrets.
        </p>
        <a href="/map" class="learn-more">En savoir plus</a>
      </div>
    </div>
  </section>

  <!-- Roadmap Section -->
  <section bind:this={sections[3].ref} class="section">
    <h2>Roadmap</h2>
    <div class="roadmap">
      <div class="roadmap-item">
        <h3>Q1 2025</h3>
        <p>Code, Prototypes, PoC</p>
      </div>
      <div class="roadmap-item">
        <h3>Q2 2025</h3>
        <p>Ajout de nouvelles quêtes et de donjons multijoueurs.</p>
      </div>
      <div class="roadmap-item">
        <h3>Q3 2025</h3>
        <p>Ouverture de la version bêta publique et optimisation des performances.</p>
      </div>
      <div class="roadmap-item">
        <h3>Q4 2025</h3>
        <p>Lancement officiel avec événements en jeu et extensions.</p>
      </div>
    </div>
  </section>
</div>