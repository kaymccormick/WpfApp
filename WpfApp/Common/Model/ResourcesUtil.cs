﻿using System.Collections ;
using System.Collections.ObjectModel ;
using System.Windows ;

namespace Common.Model
{
    /// <summary>Methods for collecting resources from WPF elements</summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for ResourcesUtil
    // ReSharper disable once UnusedType.Global
    public static class ResourcesUtil
    {
        // ReSharper disable once UnusedMember.Local
        private static void CollectResources ( FrameworkElement haveResources, ObservableCollection <ResourceInfo> ResourcesCollection  )
        {
            var res = haveResources.Resources ;
            AddResourceInfos ( res ,ResourcesCollection ) ;

            foreach ( var child in LogicalTreeHelper.GetChildren ( haveResources))
            {
                if ( child is FrameworkElement e )
                {
                    CollectResources ( e,ResourcesCollection) ;

                }
            }
        }
        private static void AddResourceInfos ( ResourceDictionary res , ObservableCollection <ResourceInfo> ResourcesCollection )
        {
            var resourcesSource = res.Source ;
            foreach ( DictionaryEntry haveResourcesResource in res )
            {
                ResourcesCollection.Add (
                                         new ResourceInfo (
                                                           resourcesSource
                                                         , haveResourcesResource.Key
                                                         , haveResourcesResource.Value
                                                          )
                                        ) ;
            }

            foreach ( var r in res.MergedDictionaries )
            {
                AddResourceInfos ( r, ResourcesCollection ) ;
            }
        }
    }
}
