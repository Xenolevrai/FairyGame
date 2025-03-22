<script lang="ts">
  // No additional logic needed
</script>

<style>
  /* Main Header Styles */
  .header {
    position: sticky;
    top: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    height: 120px;
    width: 100%;
    background-color: transparent; /* Seamless design */
    z-index: 1000; /* Keep above other content */
  }

  /* Circle Logo */
  .circle-logo {
    width: 90px;
    height: 90px;
    border-radius: 50%;
    background: url('/logo.png') no-repeat center center;
    background-size: cover;
    border: 3px solid #5b1e14;
    z-index: 2;
    position: relative;
  }

  /* Banners */
  .banner {
    flex: 1;
    height: 120px;
    background: no-repeat center center;
    background-size: contain;
    position: relative;
  }

  .banner-left {
    background-image: url('/left-banner.png');
    margin-right: -600px;
    border-top-right-radius: 50px;
  }

  .banner-right {
    background-image: url('/left-banner.png'); /* Reuse the same image */
    margin-left: -600px;
    border-top-left-radius: 50px;
    transform: scaleX(-1); /* Flip for symmetry */
  }
</style>

<header class="header">
  <div class="banner banner-left"></div>
  <a href="/" class="circle-logo" title="Accueil" aria-label="Accueil"></a>
  <div class="banner banner-right"></div>
</header>