﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infinispan.HotRod.Config;
using Infinispan.HotRod.Impl;

namespace Infinispan.HotRod
{
    /// <summary>
    ///   Factory for IRemoteCache instances.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     <b>Lifecycle:</b> In order to be able to use an IRemoteCache, the RemoteCacheManager must be started first:
    /// beside other things, this instantiates connections to Hot Rod server(s). Starting the RemoteCacheManager can be
    /// done either at creation by passing start==true to constructor or by using a constructor that does that for you
    /// (see C-tor documentation); or after construction by calling Start().
    ///   </para>
    ///   <para>
    ///     This is an "expensive" object, as it manages a set of persistent TCP connections to the Hot Rod servers.
    /// It is recommended to only have one instance of this per process, and to cache it between calls to the server
    /// (i.e. remoteCache operations).
    ///   </para>
    ///   <para>
    ///     Stop() needs to be called explicitly in order to release all the resources (e.g. threads, TCP connections).
    ///   </para>
    /// </remarks>
    public class RemoteCacheManager
    {
        private Infinispan.HotRod.SWIG.RemoteCacheManager manager;

        private Configuration configuration;
        private ISerializer serializer;

        /// <summary>
        /// Construct an instance with specific configuration and serializer.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="serializer"></param>
        public RemoteCacheManager(Configuration configuration, ISerializer serializer)
        {
            this.configuration = configuration;
            this.serializer = serializer;

            if (Infinispan.HotRod.SWIG.Util.Use64()) {
                manager = new Infinispan.HotRod.SWIG64.RemoteCacheManager((Infinispan.HotRod.SWIG64.Configuration) configuration.Config());
            } else {
                manager = new Infinispan.HotRod.SWIG32.RemoteCacheManager((Infinispan.HotRod.SWIG32.Configuration) configuration.Config());
            }
            
        }

        /// <summary>
        /// Construct an instance with specific configuration and default serializer.
        /// </summary>
        /// <param name="configuration"></param>
        public RemoteCacheManager(Configuration configuration) : this(configuration, new DefaultSerializer())
        {
        }

        /// <summary>
        /// Construct an instance with default configuration and serializer.
        /// </summary>
        public RemoteCacheManager()
        {
            if (Infinispan.HotRod.SWIG.Util.Use64()) {
                manager = new Infinispan.HotRod.SWIG64.RemoteCacheManager();
            } else {
                manager = new Infinispan.HotRod.SWIG32.RemoteCacheManager();
            }
        }

        /// <summary>
        ///   Starts the manager.
        /// </summary>
        public void Start()
        {
            manager.Start();
        }

        /// <summary>
        ///   Stops the manager.
        /// </summary>
        public void Stop()
        {
            manager.Stop();
        }

        /// <summary>
        ///   Can be used to check if the manager is started or not.
        /// </summary>
        ///     
        /// <returns>true if the cache manager is started and false otherwise</returns>
        public bool IsStarted()
        {
            return manager.IsStarted();
        }

        /// <summary>
        ///   Retrieves the default cache from the remote server.
        /// </summary>
        ///      
        /// <returns>a remote cache instance which can be used to send requests to the default cache</returns>
        public IRemoteCache<K, V> GetCache<K, V>()
        {
            if (Infinispan.HotRod.SWIG.Util.Use64()) {
                return new RemoteCacheSWIG64Impl<K, V>(manager.GetByteArrayCache(), serializer);
            } else {
                return new RemoteCacheSWIG32Impl<K, V>(manager.GetByteArrayCache(), serializer);
            }
            
        }

        /// <summary>
        ///   Retrieves a named cache from the remote server. If the cache has been previously created with the same
        ///   name, the running cache instance is returned.  Otherwise, this method attempts to create the cache first.
        /// </summary>
        ///
        /// <param name="cacheName">the name of the cache</param>
        ///
        /// <returns>a remote cache instance which can be used to send requests to the named cache</returns>
        public IRemoteCache<K, V> GetCache<K, V>(String cacheName)
        {
            if (Infinispan.HotRod.SWIG.Util.Use64()) {
                return new RemoteCacheSWIG64Impl<K, V>(manager.GetByteArrayCache(cacheName), serializer);
            } else {
                return new RemoteCacheSWIG32Impl<K, V>(manager.GetByteArrayCache(cacheName), serializer);
            }
        }

        /// <summary>
        ///   Retrieves the default cache from the remote server.
        /// </summary>
        ///
        /// <param name="forceReturnValue">indicates if the force return value flag should be enabled or not</param>
        ///
        /// <returns>a remote cache instance which can be used to send requests to the default cache</returns>
        public IRemoteCache<K, V> GetCache<K, V>(bool forceReturnValue)
        {
            if (Infinispan.HotRod.SWIG.Util.Use64()) {
                return new RemoteCacheSWIG64Impl<K, V>(manager.GetByteArrayCache(forceReturnValue), serializer);
            } else {
                return new RemoteCacheSWIG32Impl<K, V>(manager.GetByteArrayCache(forceReturnValue), serializer);
            }
        }

        /// <summary>
        ///   Retrieves a named cache from the remote server. If the cache has been previously created with the same
        ///   name, the running cache instance is returned.  Otherwise, this method attempts to create the cache first.
        /// </summary>
        ///
        /// <param name="cacheName">the name of the cache</param>
        /// <param name="forceReturnValue">indicates if the force return value flag should be enabled or not</param>
        ///
        /// <returns>a remote cache instance which can be used to send requests to the named cache</returns>
        public IRemoteCache<K, V> GetCache<K, V>(String cacheName, bool forceReturnValue)
        {
            if (Infinispan.HotRod.SWIG.Util.Use64()) {
                return new RemoteCacheSWIG64Impl<K, V>(manager.GetByteArrayCache(cacheName, forceReturnValue), serializer);
            } else {
                return new RemoteCacheSWIG32Impl<K, V>(manager.GetByteArrayCache(cacheName, forceReturnValue), serializer);
            }
        }
    }
}
