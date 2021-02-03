﻿using ImageGallery.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageGallery.API.Services
{
    public class GalleryRepository : IGalleryRepository, IDisposable
    {
        GalleryContext _context;

        public GalleryRepository(GalleryContext galleryContext)
        {
            _context = galleryContext;
        }
        public bool ImageExists(Guid id)
        {
            return _context.Images.Any(i => i.Id == id);
        }

        public bool IsOwnerOfImage(Guid imageId, string ownerId)
        {
            return _context.Images.Any(i => i.Id == imageId && i.OwnerId == ownerId);
        }
        
        public Image GetImage(Guid id)
        {
            return _context.Images.FirstOrDefault(i => i.Id == id);
        }
  
        public IEnumerable<Image> GetImages(string ownerId)
        {
            return _context.Images.Where(img=>img.OwnerId == ownerId)
                .OrderBy(i => i.Title).ToList();
        }

        public void AddImage(Image image)
        {
            _context.Images.Add(image);
        }

        public void UpdateImage(Image image)
        {
            _context.Images.Update(image);
        }

        public void DeleteImage(Image image)
        {
            _context.Images.Remove(image);

            // Note: in a real-life scenario, the image itself should also 
            // be removed from disk.  We don't do this in this demo
            // scenario, as we refill the DB with image URIs (that require
            // the actual files as well) for demo purposes.
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }

            }
        }     
    }
}
