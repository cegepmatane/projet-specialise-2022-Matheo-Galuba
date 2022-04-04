# Darwin’s battle royale arena
## Projet specialisé (2022) - MathéoGaluba

---

## Informations essentielles

- **Nom du projet :** Darwin’s battle royale arena
- **Repo github :** [lien](https://github.com/cegepmatane/projet-specialise-2022-Matheo-Galuba)
- **Aspect pricipal :** Intelligence Artificielle

## Vidéo de presentation

[![Vidéo de presentation](https://img.youtube.com/vi/L8SqIz3clD0/0.jpg)](https://www.youtube.com/watch?v=L8SqIz3clD0)

## Documentation du projet

[Lien vers la fiche de lancement "kickoff"](https://docs.google.com/document/d/1qF1iZAdF3IDPa3s8YlFjtiXWpijnrOiP3hhAlVRNXjY/edit?usp=sharing)

[Lien vers la grille de comparaison](https://docs.google.com/presentation/d/1I9EQ0xb6P17-xXolSpcOsdqIueDFJLrxob3Lt8X8flo/edit?usp=sharing)

[Lien vers la fiche de recherche successive](https://docs.google.com/presentation/d/1UeKL0SS8rF3OlXzoT8Iav-os_3cav_TlNGDJMO1TyPk/edit?usp=sharing)

[Lien vers la fiche PoC et choix technologique](https://docs.google.com/document/d/1ZCOmIQDouk4uAv60toCLpWuOyfnVU_dnbTMCDXv3HIQ/edit?usp=sharing)

[Lien vers l'analyse fonctionnelle](https://docs.google.com/presentation/d/1bdEYoB69v9VMd3qPk5wFryMiO5MysNJJ0BKXZ6yxKao/edit?usp=sharing)

## Description du projet

Mon projet est un un "zéro player game” ou “self played game” (comme le jeu de Conway), c'est-à-dire que le joueur sélectionne uniquement la situation initiale. C’est un projet d’intelligence artificielle évolutionniste : on réitère l'expérience sur plusieurs génération et chaque génération tire partie de la précédente avec certaines mutations possibles.

Il s’agit d’un environnement 3D dans lequel évolue des créatures. Le but de chaque créature est de tuer les autres à l’aide d'armes de corp à corp (des armes plus complexes pourront être implémentées par la suite) et la créature qui est la dernière à rester remporte la victoire...

Pour obliger les créatures à se battre, on pourra implémenter une “zone” qui définit l’espace dans lequel les créatures peuvent rester sans perdre de vie. Au début de la partie, la zone fera la taille totale de la map puis rétrécira avec le temps.

## Technologies utilisées

- **Moteur de rendu 3D :** [Unity](https://unity3d.com/)
- **Librairie d'apprentissage :** [PyTorch](https://pytorch.org/)
- **Librairie d'apprentissage pour Unity :** [ML-Agents](https://unity.com/fr/products/machine-learning-agents)